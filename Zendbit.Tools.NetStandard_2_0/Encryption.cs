using System.Text;
using System.Security.Cryptography;
using System;
using System.IO;

namespace Zendbit.Tools.Encryption
{
    public class AESCrypt
    {
        public static AESCrypt New() => new AESCrypt();

        private readonly Aes aesAlg = Aes.Create();

        private string FixKey(string key)
        {
            var keyByte = Encoding.UTF8.GetBytes(key);
            if (keyByte.Length < 16)
            {
                return AddKeySpan(key, 16 - keyByte.Length);
            }
            else if (keyByte.Length < 24)
            {
                return AddKeySpan(key, 24 - keyByte.Length);
            }
            else if (keyByte.Length < 32)
            {
                return AddKeySpan(key, 32 - keyByte.Length);
            }

            return key.Substring(0, 32);
        }

        private string AddKeySpan(string key, int span)
        {
            var sb = new StringBuilder(key);
            for (var i = 0; i < span; i++)
            {
                sb.Append("=");
            }
            return sb.ToString();
        }

        public string Encode(string input, string key)
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(FixKey(key));
            var iv = new byte[16];
            Array.Copy(aesAlg.Key, iv, iv.Length);
            aesAlg.IV = iv;

            string encrypted = null;

            // Create an encryptor to perform the stream transform.
            ICryptoTransform encryptor =
                aesAlg.CreateEncryptor(
                    aesAlg.Key,
                    aesAlg.IV
                );

            // Create the streams used for encryption.
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(input);
                    }
                    encrypted = Convert.ToBase64String(msEncrypt.ToArray());
                }
            }

            return encrypted;
        }

        public string Decode(string encrypted, string key)
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(FixKey(key));
            var iv = new byte[16];
            Array.Copy(aesAlg.Key, iv, iv.Length);
            aesAlg.IV = iv;

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Aes object
            // with the specified key and IV.
            // Create a decryptor to perform the stream transform.
            ICryptoTransform decryptor =
                aesAlg.CreateDecryptor(
                    aesAlg.Key,
                    aesAlg.IV
                );

            // Create the streams used for decryption.
            using (
                MemoryStream msDecrypt =
                    new MemoryStream(Convert.FromBase64String(encrypted))
            )
            {
                using (
                    CryptoStream csDecrypt =
                        new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)
                )
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {

                        // Read the decrypted bytes from the decrypting stream
                        // and place them in a string.
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }

            return plaintext;
        }

        public bool Verify(
            string input, string encrypted, string key
        )
        {
            return input.Equals(
                    Decode(encrypted, key)
                );
        }
    }

    public class MD5Crypt
    {
        public static MD5Crypt New() => new MD5Crypt();

        private readonly MD5 md5Hash = MD5.Create();

        public string ComputeHash(string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString().ToLower();
        }

        public bool Verify(string input, string hashed)
        {
            return hashed.ToLower()
                .Equals(ComputeHash(input).ToLower());
        }
    }

    public class SHA1Crypt
    {
        private readonly SHA1 sha = new SHA1CryptoServiceProvider();

        public static SHA1Crypt New() => new SHA1Crypt();

        public byte[] ComputeHash(byte[] input)
        {
            return sha.ComputeHash(input);
        }

        public string ComputeHash(string input)
        {
            return BitConverter
                .ToString(
                    ComputeHash(
                        Encoding.UTF8.GetBytes(input)
                    )
                )
                .Replace("-", "")
                .ToLower();
        }

        public bool Verify(
            string input, string hashed
        )
        {
            return hashed.ToLower()
                .Equals(ComputeHash(input));
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

        public string Encode(string input, string key)
        {
            var encrypted = Cipher(input, key);
            return Base64Crypt.New().Encode(encrypted);
        }

        public string Decode(string encrypted, string key)
        {
            var decode = Base64Crypt.New().Decode(encrypted);
            return Cipher(decode, key);
        }

        public bool Verify(
            string input,
            string encrypted,
            string key
        )
        {
            return input.Equals(
                Decode(encrypted, key)
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

        public bool Verify(string input, string encoded)
        {
            return input.Equals(Decode(encoded));
        }
    }
}
