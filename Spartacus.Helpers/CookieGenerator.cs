using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Spartacus.Helpers
{
    public static class CookieGenerator
    {
        private const string SaltData = "QADLz4qk3rVgBSGjDfAH3XWV" + "qKKagMXezBPv7TmXvwnXDDeR" + "pHaLBv4JnTGRwLg9tzbmV77g" + "8DUEAEa6JPv66hy7SwHBL4z4" + "FbGdh2MVs4kq9RcaZEAszuP5"
                                        + "ccLsEfqCpwdSvVVt479DCZrw" + "jSHrJVwaja9WQaWAmEY9NsPv" + "EHKnFwHTGAvPXpjpCxkbedYq" + "uEauLvZLphwmJLUteZ4QAXU6" + "Z4F3PDmh3wsQXvSctQBHvNWf";
        private static readonly byte[] Salt = Encoding.ASCII.GetBytes(SaltData);

        /// <summary>
		/// Encrypt the given string using AES.  The string can be decrypted using 
		/// DecryptStringAES().  The sharedSecret parameters must match.
		/// </summary>
		/// <param name="plainText">The text to encrypt.</param>
		/// <param name="sharedSecret">A password used to generate a key for encryption.</param>
        public static string EncryptStringAes(string plainText, string sharedSecret)
        {
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentNullException(nameof(plainText));
            if (string.IsNullOrEmpty(sharedSecret))
                throw new ArgumentNullException(nameof(sharedSecret));

            string outStr;
            using var aesAlg = Aes.Create();

            var key = new Rfc2898DeriveBytes(sharedSecret, Salt);
            aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);

            var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            using (var msEncrypt = new MemoryStream())
            {
                // prepend the IV and it's size
                msEncrypt.Write(BitConverter.GetBytes(aesAlg.IV.Length), 0, sizeof(int));
                msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);

                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using var swEncrypt = new StreamWriter(csEncrypt);
                    swEncrypt.Write(plainText);
                }
                outStr = Convert.ToBase64String(msEncrypt.ToArray());
            }

            return outStr;
        }

        /// <summary>
        /// Decrypt the given string.  Assumes the string was encrypted using 
        /// EncryptStringAES(), using an identical sharedSecret.
        /// </summary>
        /// <param name="cipherText">The text to decrypt.</param>
        /// <param name="sharedSecret">A password used to generate a key for decryption.</param>
        public static string DecryptStringAes(string cipherText, string sharedSecret)
        {
            if (string.IsNullOrEmpty(cipherText))
                throw new ArgumentNullException(nameof(cipherText));
            if (string.IsNullOrEmpty(sharedSecret))
                throw new ArgumentNullException(nameof(sharedSecret));

            Aes aesAlg = Aes.Create();
            string plaintext;

            var key = new Rfc2898DeriveBytes(sharedSecret, Salt);

            var bytes = Convert.FromBase64String(cipherText);
            using (var msDecrypt = new MemoryStream(bytes))
            {
                aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
                aesAlg.IV = ReadByteArray(msDecrypt);
                var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                using var srDecrypt = new StreamReader(csDecrypt);

                plaintext = srDecrypt.ReadToEnd();
            }

            return plaintext;
        }

        private static byte[] ReadByteArray(Stream s)
        {
            var rawLength = new byte[sizeof(int)];
            if (s.Read(rawLength, 0, rawLength.Length) != rawLength.Length)
            {
                throw new SystemException("Stream did not contain properly formatted byte array");
            }

            var buffer = new byte[BitConverter.ToInt32(rawLength, 0)];
            if (s.Read(buffer, 0, buffer.Length) != buffer.Length)
            {
                throw new SystemException("Did not read byte array properly");
            }

            return buffer;
        }
    }
}
