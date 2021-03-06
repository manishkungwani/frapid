﻿namespace Frapid.Account
{
    public static class PasswordManager
    {
        public static bool ValidateBcrypt(string plainPassword, string hashedPassword)
        {
            if (plainPassword == null || string.IsNullOrWhiteSpace(hashedPassword))
            {
                return false;
            }

            return BCrypt.Net.BCrypt.Verify(plainPassword, hashedPassword);
        }

        public static string GetHashedPassword(string password)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt(10);
            return BCrypt.Net.BCrypt.HashPassword(password, salt);
        }
    }
}