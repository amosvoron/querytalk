using QueryTalk.Globals;

namespace QueryTalk.Security
{
    internal class CryptoContext
    {
        private static string _rsainternal = "";
        private static string _rsaPrivate = "";
        private static string _rsaCryptedSimKey = "";

        internal static string ComunicationKey
        {
            get { return GlobalResources.ComunicationKey; }
        }

        internal static string RSAinternalKey
        {
            get
            {
                if (string.IsNullOrEmpty(_rsainternal))
                {
                    CryptographyManager objQueryTalkCryptography = new CryptographyManager();

                    _rsainternal = objQueryTalkCryptography.CreateinternalKeyAsim();
                    _rsaPrivate = objQueryTalkCryptography.CreatePrivateKeyAsim();
                }
                return _rsainternal;
            }
        }
        internal static string RSAPrivateKey
        {
            get
            {
                if (string.IsNullOrEmpty(_rsaPrivate))
                {
                    CryptographyManager objQueryTalkCryptography = new CryptographyManager();

                    _rsaPrivate = objQueryTalkCryptography.CreatePrivateKeyAsim();
                    _rsainternal = objQueryTalkCryptography.CreateinternalKeyAsim();
                }
                return _rsaPrivate;
            }
        }
        internal static string CryptedSimKey
        {
            get
            {
                return _rsaCryptedSimKey;
            }
            set
            {
                _rsaCryptedSimKey = value;
            }
        }
    }
}
