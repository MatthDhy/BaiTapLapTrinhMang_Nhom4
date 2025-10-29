using System;
using System.Text.RegularExpressions;

namespace UserManageApp.Utils
{
    public static class Validator
    {
        // Username: không trống, dài >= 3 ký tự
        public static bool IsValidUsername(string username, out string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                errorMessage = "Username không được để trống.";
                return false;
            }

            if (username.Length < 3)
            {
                errorMessage = "Username phải có ít nhất 3 ký tự.";
                return false;
            }

            errorMessage = string.Empty;
            return true;
        }

        // Password: tối thiểu 6 ký tự
        public static bool IsValidPassword(string password, out string errorMessage)
        {
            if (string.IsNullOrEmpty(password) || password.Length < 6)
            {
                errorMessage = "Password phải có ít nhất 6 ký tự.";
                return false;
            }

            errorMessage = string.Empty;
            return true;
        }

        // Confirm Password phải khớp
        public static bool IsPasswordConfirmed(string password, string confirm, out string errorMessage)
        {
            if (password != confirm)
            {
                errorMessage = "Password và Confirm Password không khớp.";
                return false;
            }

            errorMessage = string.Empty;
            return true;
        }

        // Email: regex chuẩn
        public static bool IsValidEmail(string email, out string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                errorMessage = "Email không được để trống.";
                return false;
            }

            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(email, pattern))
            {
                errorMessage = "Email không hợp lệ (vd: abc@xyz.com).";
                return false;
            }

            errorMessage = string.Empty;
            return true;
        }
    }
}
