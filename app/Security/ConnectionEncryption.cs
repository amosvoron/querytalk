using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace QueryTalk.Mapper
{
    internal class ConnectionEncryption
    {
        private static readonly string AES_KEY = "wEZNXejUK0VkNrCTXVl/9r2NMES8gS43PohuBOkPtBI=";
        private static readonly string AES_IV = "aZv/pI9mtoVmmR9LbIuGXA==";

        // encrypt
        internal static string EncryptAes(string value)
        {
            // check argument
            if (value == null || value.Length == 0)
            {
                return null;
            }

            byte[] encrypted;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Convert.FromBase64String(AES_KEY);
                aesAlg.IV = Convert.FromBase64String(AES_IV);

                // create a decrytor to perform the stream transform
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // create the streams used for encryption
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(value);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(encrypted);
        }

        // decrypt
        internal static string DecryptAes(string encrypted)
        {
            // check argument
            if (encrypted == null || encrypted.Length == 0)
            {
                return null;
            }

            string plaintext = null;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Convert.FromBase64String(AES_KEY);
                aesAlg.IV = Convert.FromBase64String(AES_IV);

                // create a decrytor to perform the stream transform
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // create the streams used for decryption
                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(encrypted)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;
        }

    }
}
