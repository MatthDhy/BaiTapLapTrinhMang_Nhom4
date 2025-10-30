﻿using System.Security.Cryptography;
using System.Text;

namespace UserManageApp.Utils
{
    public static class Security
    {
            public static string HashPassword(string password)
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(password);
                    byte[] hash = sha256.ComputeHash(bytes);

                    StringBuilder sb = new StringBuilder();
                    foreach (var b in hash)
                    {
                        sb.Append(b.ToString("x2"));
                    }
                    return sb.ToString();
                }
            }
        }
    }
