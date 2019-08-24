using System;
using Zendbit.Tools.Encryption;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace TestCase
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AES Test Test...");
            Console.WriteLine("=================");
            var originalAes = "Original AES Text...";
            var aesKey = "S3cr3tK3y";
            var aesEncoded = AESCrypt.New().Encode(
                originalAes,
                aesKey
            );

            var aesDecoded = AESCrypt.New().Decode(
                aesEncoded,
                aesKey
            );

            var aesVerified = AESCrypt.New().Verify(originalAes, aesEncoded, aesKey);

            Console.WriteLine($"Aes original {originalAes} \n Aes encoded {aesEncoded} \n Aes Decoded {aesDecoded} \n Verified {aesVerified}");
            Console.WriteLine("=================\n\n");


            Console.WriteLine("MD5 Test Test...");
            Console.WriteLine("=================");
            var originalMD5 = "Original Md5 Text...";
            var MD5Encoded = MD5Crypt.New().ComputeHash(
                originalMD5
            );

            var MD5Verified = MD5Crypt.New().Verify(originalMD5, MD5Encoded);

            Console.WriteLine($"Md5 original {originalMD5} \n MD5Hash {MD5Encoded} \n verified {MD5Verified}");
            Console.WriteLine("=================\n\n");


            Console.WriteLine("SHA1 Test Test...");
            Console.WriteLine("=================");
            var originalSHA1 = "Original SHA1 Text...";
            var sha1Encoded = SHA1Crypt.New().ComputeHash(
                originalSHA1
            );

            var sha1Verified = SHA1Crypt.New().Verify(originalSHA1, sha1Encoded);

            Console.WriteLine($"sha1 original {originalSHA1} \n sha1 hash {sha1Encoded} \n verified {sha1Verified}");
            Console.WriteLine("=================\n\n");


            Console.WriteLine("XOR Test Test...");
            Console.WriteLine("=================");
            var originalXor = "Original XOR Text...";
            var xorKey = "S3cr3tK3y";
            var encodedXor = XORCrypt.New().Encode(
                originalXor,
                xorKey
            );

            var xorVerified = XORCrypt.New().Verify(originalXor, encodedXor, xorKey);

            var xorDecoded = XORCrypt.New().Decode(
                encodedXor,
                xorKey
            );

            Console.WriteLine($"Xor original {originalXor} \n Xor encoded {encodedXor} \n Xor Decoded {xorDecoded} \n Verified {xorVerified}");
            Console.WriteLine("=================\n\n");


            Console.WriteLine("Base64 Test Test...");
            Console.WriteLine("=================");
            var originalBase64 = "Original Base64 Text...";
            var base64Encoded = Base64Crypt.New().Encode(
                originalBase64);

            var base64Decoded = Base64Crypt.New().Decode(
                base64Encoded);

            var base64Verified = Base64Crypt.New().Verify(originalBase64, base64Encoded);

            Console.WriteLine($"base64 original {originalBase64} \n base64 encoded {base64Encoded} \n base64 decoded {base64Decoded} \n verified {base64Verified}");
            Console.WriteLine("=================\n\n");
        }
    }
}
