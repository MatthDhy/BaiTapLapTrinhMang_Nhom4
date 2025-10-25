using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Server.Models;

namespace Server
{
    public class ServerCore
    {
        public event Action<string> OnLog;
        public event Action<string> OnClientConnected;
        public event Action<string> OnClientDisconnected;

        private readonly int _port;
        private readonly DbHelper _db;
        private TcpListener _listener;
        private bool _running = false;

        // Dùng Tuple cổ điển thay cho tuple mới của C# 8
        private ConcurrentDictionary<string, Tuple<User, DateTime>> _tokens =
            new ConcurrentDictionary<string, Tuple<User, DateTime>>();

        public ServerCore(int port, string connStr)
        {
            _port = port;
            _db = new DbHelper(connStr);
        }

        public void Start()
        {
            try
            {
                _listener = new TcpListener(IPAddress.Any, _port);
                _listener.Start();
                _running = true;
                OnLog?.Invoke("Listening on port " + _port);
                AcceptLoop();
            }
            catch (Exception ex)
            {
                OnLog?.Invoke("Start error: " + ex.Message);
            }
        }

        private async void AcceptLoop()
        {
            while (_running)
            {
                try
                {
                    var client = await _listener.AcceptTcpClientAsync();
                    string remote = client.Client.RemoteEndPoint != null
                        ? client.Client.RemoteEndPoint.ToString()
                        : "unknown";
                    OnClientConnected?.Invoke(remote);
                    Task.Run(() => HandleClient(client, remote));
                }
                catch (ObjectDisposedException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    OnLog?.Invoke("Accept error: " + ex.Message);
                }
            }
        }

        private async Task HandleClient(TcpClient client, string remote)
        {
            NetworkStream ns = null;
            StreamReader reader = null;
            StreamWriter writer = null;

            try
            {
                ns = client.GetStream();
                reader = new StreamReader(ns, Encoding.UTF8);
                writer = new StreamWriter(ns, Encoding.UTF8);
                writer.AutoFlush = true;

                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    OnLog?.Invoke("Recv [" + remote + "]: " + line);
                    var req = JsonSerializer.Deserialize<RequestMessage>(
                        line,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                    );
                    var resp = await ProcessRequest(req);
                    string respJson = JsonSerializer.Serialize(resp);
                    await writer.WriteLineAsync(respJson);
                    OnLog?.Invoke("Sent [" + remote + "]: " + respJson);
                }
            }
            catch (Exception ex)
            {
                OnLog?.Invoke("Client error (" + remote + "): " + ex.Message);
            }
            finally
            {
                if (writer != null) writer.Dispose();
                if (reader != null) reader.Dispose();
                if (ns != null) ns.Dispose();
                client.Close();
                OnClientDisconnected?.Invoke(remote);
            }
        }

        private Task<ResponseMessage> ProcessRequest(RequestMessage req)
        {
            if (req == null || string.IsNullOrEmpty(req.Action))
            {
                return Task.FromResult(new ResponseMessage
                {
                    Success = false,
                    Message = "Invalid request"
                });
            }

            string action = req.Action.ToUpper();

            switch (action)
            {
                case "REGISTER":
                    return HandleRegister(req);
                case "LOGIN":
                    return HandleLogin(req);
                case "GETINFO":
                    return HandleGetInfo(req);
                case "LOGOUT":
                    return HandleLogout(req);
                default:
                    return Task.FromResult(new ResponseMessage
                    {
                        Success = false,
                        Message = "Unknown action"
                    });
            }
        }

        private Task<ResponseMessage> HandleRegister(RequestMessage req)
        {
            try
            {
                var dto = JsonSerializer.Deserialize<RegisterDto>(
                    req.Data.GetRawText(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );

                if (dto == null || string.IsNullOrWhiteSpace(dto.Username) || string.IsNullOrWhiteSpace(dto.Password))
                    return Task.FromResult(new ResponseMessage { Success = false, Message = "Username and password required" });

                if (_db.UsernameExists(dto.Username))
                    return Task.FromResult(new ResponseMessage { Success = false, Message = "Username already exists" });

                DateTime? bd = null;
                DateTime d;
                if (DateTime.TryParse(dto.Birthday, out d)) bd = d;

                var user = new User
                {
                    Username = dto.Username,
                    Password = dto.Password,
                    Email = dto.Email,
                    FullName = dto.FullName,
                    Birthday = bd
                };

                bool ok = _db.CreateUser(user);
                return Task.FromResult(new ResponseMessage
                {
                    Success = ok,
                    Message = ok ? "Register success" : "Register failed"
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseMessage
                {
                    Success = false,
                    Message = "Error: " + ex.Message
                });
            }
        }

        private Task<ResponseMessage> HandleLogin(RequestMessage req)
        {
            try
            {
                var dto = JsonSerializer.Deserialize<LoginDto>(
                    req.Data.GetRawText(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );

                var user = _db.GetUserByCredentials(dto.Username, dto.Password);
                if (user == null)
                    return Task.FromResult(new ResponseMessage { Success = false, Message = "Invalid username or password" });

                string token = Guid.NewGuid().ToString("N");
                DateTime expiry = DateTime.UtcNow.AddHours(1);
                _tokens[token] = new Tuple<User, DateTime>(user, expiry);

                var data = new
                {
                    Token = token,
                    ExpiresAt = expiry,
                    User = new
                    {
                        user.UserId,
                        user.Username,
                        user.Email,
                        user.FullName,
                        Birthday = user.Birthday.HasValue ? user.Birthday.Value.ToString("yyyy-MM-dd") : null
                    }
                };

                string json = JsonSerializer.Serialize(data);
                return Task.FromResult(new ResponseMessage
                {
                    Success = true,
                    Message = "Login success",
                    Data = JsonDocument.Parse(json).RootElement
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ResponseMessage
                {
                    Success = false,
                    Message = "Error: " + ex.Message
                });
            }
        }

        private Task<ResponseMessage> HandleGetInfo(RequestMessage req)
        {
            var dto = JsonSerializer.Deserialize<TokenDto>(req.Data.GetRawText());
            if (dto == null || string.IsNullOrEmpty(dto.Token))
                return Task.FromResult(new ResponseMessage { Success = false, Message = "Invalid token" });

            Tuple<User, DateTime> info;
            if (!_tokens.TryGetValue(dto.Token, out info))
                return Task.FromResult(new ResponseMessage { Success = false, Message = "Invalid token" });

            if (info.Item2 < DateTime.UtcNow)
            {
                Tuple<User, DateTime> removed;
                _tokens.TryRemove(dto.Token, out removed);
                return Task.FromResult(new ResponseMessage { Success = false, Message = "Token expired" });
            }

            var u = info.Item1;
            var data = new
            {
                u.UserId,
                u.Username,
                u.Email,
                u.FullName,
                Birthday = u.Birthday.HasValue ? u.Birthday.Value.ToString("yyyy-MM-dd") : null
            };

            string json = JsonSerializer.Serialize(data);
            return Task.FromResult(new ResponseMessage
            {
                Success = true,
                Message = "OK",
                Data = JsonDocument.Parse(json).RootElement
            });
        }

        private Task<ResponseMessage> HandleLogout(RequestMessage req)
        {
            var dto = JsonSerializer.Deserialize<TokenDto>(req.Data.GetRawText());
            if (dto == null) return Task.FromResult(new ResponseMessage { Success = false, Message = "Invalid token" });

            Tuple<User, DateTime> removed;
            _tokens.TryRemove(dto.Token, out removed);
            return Task.FromResult(new ResponseMessage { Success = true, Message = "Logged out" });
        }

        public void Stop()
        {
            _running = false;
            try
            {
                if (_listener != null)
                    _listener.Stop();
                OnLog?.Invoke("Listener stopped.");
            }
            catch (Exception ex)
            {
                OnLog?.Invoke("Stop error: " + ex.Message);
            }
        }
    }
}
