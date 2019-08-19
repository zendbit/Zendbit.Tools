using System.Text;
using System.Security.Cryptography;
using System;

namespace Zendbit.Tools.Encryption
{
    public class SHA1Crypt
    {
        private readonly SHA1 sha = new SHA1CryptoServiceProvider();

        public static SHA1Crypt New()
        {
            return new SHA1Crypt();
        }

        public byte[] ComputeHash(byte[] data)
        {
            return sha.ComputeHash(data);
        }

        public string ComputeHash(string data)
        {
            return BitConverter
                .ToString(
                ComputeHash(
                    Encoding.UTF8.GetBytes(data)
                    )
                )
                .Replace("-", "");
        }
    }

    public class XORCrypt
    {
        public static XORCrypt New() => new XORCrypt();
        private string Cipher(string data, string key)
        {
            int dataLen = data.Length;
            int keyLen = key.Length;
            char[] output = new char[dataLen];

            for (int i = 0; i < dataLen; ++i)
            {
                output[i] = (char)(data[i] ^ key[i % keyLen]);
            }

            return new string(output);
        }

        public string Encode(string data, string key)
        {
            return Cipher(
                Base64Crypt.New().Encode(
                    data
                ),
                key
            );
        }

        public string Decode(string cipher, string key)
        {
            return Base64Crypt.New().Decode(
                Cipher(
                    cipher, key
                )
            );
        }
    }

    public class Base64Crypt
    {
        public static Base64Crypt New() => new Base64Crypt();

        public string Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public string Decode(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
