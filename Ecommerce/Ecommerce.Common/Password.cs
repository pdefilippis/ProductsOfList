using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Ecommerce.Common
{
    public class Password
    {
        public static string EncryptPassword(string password)
        {
            var provider = new SHA256CryptoServiceProvider();

            byte[] inputBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashedBytes = provider.ComputeHash(inputBytes);

            var output = new StringBuilder();

            for (int i = 0; i < hashedBytes.Length; i++)
                output.Append(hashedBytes[i].ToString("x2").ToLower());

            return output.ToString();
        }
    }
}
