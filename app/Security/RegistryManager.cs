using System.Collections.Generic;
using Microsoft.Win32;
using QueryTalk.Globals;

namespace QueryTalk.Security
{
    //https://msdn.microsoft.com/es-es/library/h5e7chcf.aspx
    internal class RegistryManager
    {
        internal void SaveCredencials(string user, string password, bool rememberMe)
        {
            RegistryKey key = GetCredentialRoute();

            CryptographyManager objQueryTalkCryptography = new CryptographyManager();
            //string decryptedsimkey = objQueryTalkCryptography.DecryptAsim(CryptoContext.CryptedSimKey, CryptoContext.RSAPrivateKey);

            key.SetValue(GlobalResources.Key1, objQueryTalkCryptography.EncryptSim(user, CryptoContext.ComunicationKey), RegistryValueKind.String);
            key.SetValue(GlobalResources.Key2, objQueryTalkCryptography.EncryptSim(password, CryptoContext.ComunicationKey), RegistryValueKind.String);
            key.SetValue(GlobalResources.Key3, objQueryTalkCryptography.EncryptSim(rememberMe.ToString(), CryptoContext.ComunicationKey), RegistryValueKind.String);
        }

        internal List<string> ReadCredencial()
        {
            return GetKeyCredentials();
        }

        internal void ClearCredentials()
        {
            RegistryKey keyQueryTalk = GetQueryTalkRoute();
            keyQueryTalk.DeleteSubKeyTree(GlobalResources.Level3, false);
        }

        private RegistryKey GetQueryTalkRoute()
        {
            RegistryKey keyCurrentUser = Registry.CurrentUser;
            RegistryKey keySOFTWARE = keyCurrentUser.OpenSubKey(GlobalResources.Level1, true);
            RegistryKey keyQueryTalk = keySOFTWARE.OpenSubKey(GlobalResources.Level2, true);

            if (keyQueryTalk == null)
            {
                keyQueryTalk = keySOFTWARE.CreateSubKey(GlobalResources.Level2);
            }

            return keyQueryTalk;
        }

        private RegistryKey GetCredentialRoute()
        {
            RegistryKey keyQueryTalk = GetQueryTalkRoute();
            RegistryKey keyCredential = keyQueryTalk.OpenSubKey(GlobalResources.Level3, true);

            if (keyCredential == null)
            {
                keyCredential = keyQueryTalk.CreateSubKey(GlobalResources.Level3);
            }

            return keyCredential;
        }

        private List<string> GetKeyCredentials()
        {
            RegistryKey keyCredential = GetCredentialRoute();

            string credential1 = (string)keyCredential.GetValue(GlobalResources.Key1);
            string credential2 = (string)keyCredential.GetValue(GlobalResources.Key2);
            string credential3 = (string)keyCredential.GetValue(GlobalResources.Key3);

            if (credential1 != null && credential2 != null && credential3 != null)
            {
                CryptographyManager objQueryTalkCryptography = new CryptographyManager();
                //string decryptedsimkey = objQueryTalkCryptography.DecryptAsim(CryptoContext.CryptedSimKey, CryptoContext.RSAPrivateKey);

                string user = objQueryTalkCryptography.DecryptSim(credential1, CryptoContext.ComunicationKey);
                string password = objQueryTalkCryptography.DecryptSim(credential2, CryptoContext.ComunicationKey);
                string rememberMe = objQueryTalkCryptography.DecryptSim(credential3, CryptoContext.ComunicationKey);

                List<string> Keys = new List<string>() { user, password, rememberMe };

                return Keys;
            }
            else
            {
                return new List<string> { "", "", "" };
            }
        }

        internal bool HasAccount
        {
            get
            {
                var keyQueryTalk = GetQueryTalkRoute();
                var hasAccount = keyQueryTalk.GetValue(GlobalResources.HasAccountKey);

                // anonymous: HasAccount key does not exists
                // registered user: HasAccountKey exists, any value is valid
                return hasAccount != null;
            }
        }

        internal void RegisterAccount()
        {
            RegistryKey key = GetQueryTalkRoute();
            key.SetValue(GlobalResources.HasAccountKey, "true");
        }

    }
}
