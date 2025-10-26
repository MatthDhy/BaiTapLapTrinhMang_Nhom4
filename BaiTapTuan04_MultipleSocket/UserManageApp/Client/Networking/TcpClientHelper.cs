using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class TcpClientHelper : IDisposable
    {
        private TcpClient _client;
        private NetworkStream _stream;


        public bool IsConnected => _client != null && _client.Connected;

        public async Task ConnectAsync(string host, int port)
        {
            if (_client == null || !_client.Connected)
            {
                _client = new TcpClient();
                await _client.ConnectAsync(host, port);
                _stream = _client.GetStream();
            }
        }


        public async Task<string> SendAsync(string message)
        {
            if (_stream == null)
                throw new InvalidOperationException("Chưa kết nối đến server.");

            byte[] data = Encoding.UTF8.GetBytes(message + "\n");
            await _stream.WriteAsync(data, 0, data.Length);

            // Đọc phản hồi từ server (tùy theo logic server của bạn)
            byte[] buffer = new byte[4096];
            int bytesRead = await _stream.ReadAsync(buffer, 0, buffer.Length);
            string response = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();

            return response;
        }

        public void Dispose()
        {
            if (_stream != null)
            {
                _stream.Close();
                _stream.Dispose();
                _stream = null;
            }

            if (_client != null)
            {
                _client.Close();
                _client = null;
            }
        }
    }
}
