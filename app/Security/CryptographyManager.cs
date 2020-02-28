using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace QueryTalk.Security
{
    internal class CryptographyManager
    {
        #region Encriptación simétrica Rijndael
        //Documentación tomada del blog listado debajo
        //El vectr IV se ha exraido de la misma clave pero debería ser un dato generado aparte.
        //Tanto la clave como el vector IV pueden ser extraidos de la propia implementación de .Net Framework
        //la dificultad radica en almacenar dichos valores en un lugar seguro hasta el próximo proceso criptográfico
        //http://florentinoh.blogspot.com.es/2015/06/cifrado-y-descifrado-simetrico-con.html
        internal string EncryptSim(String plainMessage, String strPK)
        {
            byte[] key = UTF8Encoding.UTF8.GetBytes(strPK);
            byte[] iv = UTF8Encoding.UTF8.GetBytes(strPK); //strIv

            Array.Resize(ref key, 32);
            Array.Resize(ref iv, 16);

            return encryptStringSim(plainMessage, key, iv);
        }
        internal string DecryptSim(String encryptedMessage, String strPK)
        {
            byte[] key = UTF8Encoding.UTF8.GetBytes(strPK);
            byte[] iv = UTF8Encoding.UTF8.GetBytes(strPK); //strIv

            Array.Resize(ref key, 32);
            Array.Resize(ref iv, 16);

            return decryptStringSim(encryptedMessage, key, iv);
        }

        private string encryptStringSim(String plainMessage, byte[] Key, byte[] IV)
        {
            Rijndael RijndaelAlg = Rijndael.Create();

            plainMessage = plainMessage ?? string.Empty;

            // added by amos (para lidiar con "Padding is invalid and cannot be removed." exception)
            // ATENCIÓN: si se define añade un sufijo que "ensucia" el mensaje encriptado/decriptado
            // RijndaelAlg.Padding = PaddingMode.None; 

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, RijndaelAlg.CreateEncryptor(Key, IV), CryptoStreamMode.Write))
                {
                    byte[] plainMessageBytes = UTF8Encoding.UTF8.GetBytes(plainMessage);
                    cryptoStream.Write(plainMessageBytes, 0, plainMessageBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    byte[] cipherMessageBytes = memoryStream.ToArray();
                    //memoryStream.Close();
                    //cryptoStream.Close();
                    return Convert.ToBase64String(cipherMessageBytes);
                }
            }
        }
        private string decryptStringSim(String encryptedMessage, byte[] Key, byte[] IV)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(encryptedMessage);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            Rijndael RijndaelAlg = Rijndael.Create();

            // added by amos (para lidiar con "Padding is invalid and cannot be removed." exception)
            // ATENCIÓN: si se define añade un sufijo que "ensucia" el mensaje encriptado/decriptado
            // RijndaelAlg.Padding = PaddingMode.None; 

            using (var memoryStream = new MemoryStream(cipherTextBytes))
            {
                using (var cryptoStream = new CryptoStream(memoryStream, RijndaelAlg.CreateDecryptor(Key, IV), CryptoStreamMode.Read))
                {
                    int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                    //memoryStream.Close();
                    //cryptoStream.Close();
                    return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                }
            }
        }
        #endregion

        #region Encriptación asimétrica RSA
        private RSACryptoServiceProvider _RSAService;

        private RSACryptoServiceProvider RSAService
        {
            get
            {
                if (_RSAService == null)
                    _RSAService = new RSACryptoServiceProvider();
                return _RSAService;
            }
        }

        internal string CreateinternalKeyAsim()
        {
            string xmlinternalKey = RSAService.ToXmlString(false);
            return xmlinternalKey;
            //return Encoding.ASCII.GetBytes(xmlinternalKey);
        }
        internal string CreatePrivateKeyAsim()
        {
            string xmlPrivateKey = RSAService.ToXmlString(true);
            return xmlPrivateKey;
            //return Encoding.ASCII.GetBytes(xmlPrivateKey);
        }
        internal string EncryptAsim(String plainMessage, String strPK)
        {
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(2048);
            RSA.FromXmlString(strPK);
            byte[] encryptedData = RSA.Encrypt(Encoding.ASCII.GetBytes(plainMessage), false);
            return Convert.ToBase64String(encryptedData);
        }
        internal string DecryptAsim(String encryptedMessage, String strPK)
        {
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(2048);
            RSA.FromXmlString(strPK);
            byte[] encryptedData = RSA.Decrypt(Convert.FromBase64String(encryptedMessage), false);
            return Encoding.ASCII.GetString(encryptedData);
        }
        #endregion

        #region Hash
        internal string GetStringSha256Hash(string text)
        {
            if (String.IsNullOrEmpty(text))
                return String.Empty;

            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                byte[] textData = System.Text.Encoding.UTF8.GetBytes(text);
                byte[] hash = sha.ComputeHash(textData);
                return BitConverter.ToString(hash).Replace("-", String.Empty);
            }
        }
        #endregion
    }

    internal class Encryption
    {
        #region Encriptación simétrica Rijndael
        //Documentación tomada del blog listado debajo
        //El vectr IV se ha exraido de la misma clave pero debería ser un dato generado aparte.
        //Tanto la clave como el vector IV pueden ser extraidos de la propia implementación de .Net Framework
        //la dificultad radica en almacenar dichos valores en un lugar seguro hasta el próximo proceso criptográfico
        //http://florentinoh.blogspot.com.es/2015/06/cifrado-y-descifrado-simetrico-con.html
        internal static string EncryptSim(String plainMessage, String strPK)
        {
            byte[] key = UTF8Encoding.UTF8.GetBytes(strPK);
            byte[] iv = UTF8Encoding.UTF8.GetBytes(strPK); //strIv

            Array.Resize(ref key, 32);
            Array.Resize(ref iv, 16);

            return encryptStringSim(plainMessage, key, iv);
        }
        internal static string DecryptSim(String encryptedMessage, String strPK)
        {
            byte[] key = UTF8Encoding.UTF8.GetBytes(strPK);
            byte[] iv = UTF8Encoding.UTF8.GetBytes(strPK); //strIv

            Array.Resize(ref key, 32);
            Array.Resize(ref iv, 16);

            return decryptStringSim(encryptedMessage, key, iv);
        }

        private static string encryptStringSim(String plainMessage, byte[] Key, byte[] IV)
        {
            Rijndael RijndaelAlg = Rijndael.Create();
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, RijndaelAlg.CreateEncryptor(Key, IV), CryptoStreamMode.Write);
            byte[] plainMessageBytes = UTF8Encoding.UTF8.GetBytes(plainMessage);
            cryptoStream.Write(plainMessageBytes, 0, plainMessageBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] cipherMessageBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            return Convert.ToBase64String(cipherMessageBytes);
        }
        private static string decryptStringSim(String encryptedMessage, byte[] Key, byte[] IV)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(encryptedMessage);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            Rijndael RijndaelAlg = Rijndael.Create();
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, RijndaelAlg.CreateDecryptor(Key, IV), CryptoStreamMode.Read);
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }
        #endregion

        #region Encriptación asimétrica RSA
        private static RSACryptoServiceProvider _RSAService;

        private static RSACryptoServiceProvider RSAService
        {
            get
            {
                if (_RSAService == null)
                    _RSAService = new RSACryptoServiceProvider();
                return _RSAService;
            }
        }

        internal static string CreateinternalKeyAsim()
        {
            string xmlinternalKey = RSAService.ToXmlString(false);
            return xmlinternalKey;
        }
        internal static string CreatePrivateKeyAsim()
        {
            string xmlPrivateKey = RSAService.ToXmlString(true);
            return xmlPrivateKey;
            //return Encoding.ASCII.GetBytes(xmlPrivateKey);
        }
        internal static string EncryptAsim(String plainMessage, String strPK)
        {
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(2048);
            RSA.FromXmlString(strPK);
            byte[] encryptedData = RSA.Encrypt(Encoding.ASCII.GetBytes(plainMessage), false);
            return Convert.ToBase64String(encryptedData);
        }
        internal static string DecryptAsim(String encryptedMessage, String strPK)
        {
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(2048);
            RSA.FromXmlString(strPK);
            byte[] encryptedData = RSA.Decrypt(Convert.FromBase64String(encryptedMessage), false);
            return Encoding.ASCII.GetString(encryptedData);
        }
        #endregion

        #region Hash
        internal static string GetStringSha256Hash(string text)
        {
            if (String.IsNullOrEmpty(text))
                return String.Empty;

            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                byte[] textData = System.Text.Encoding.UTF8.GetBytes(text);
                byte[] hash = sha.ComputeHash(textData);
                return BitConverter.ToString(hash).Replace("-", String.Empty);
            }
        }
        #endregion
    }

}
