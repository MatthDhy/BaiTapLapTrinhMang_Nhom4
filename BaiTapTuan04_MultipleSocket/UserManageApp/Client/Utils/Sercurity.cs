using System.Security.Cryptography;
using System.Text;

namespace Exercise3_Client.Utils
{
    public static class Security
    {
        public static string Sha256Hex(string text)
        {
            using (var sha265 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(text);
                var hash = sha265.ComputeHash(bytes);
                var sb = new StringBuilder();
                foreach (var b in hash) sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }
    }
}
