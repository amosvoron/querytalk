using System.IO;
using System.Net;
using System.Collections.Specialized;
using QueryTalk.Globals;

namespace QueryTalk.Security
{
    internal class ClientDownload
    {
        private string _communicationID;
        private string _username;
        private string _password;

        internal byte[] LibBytes { get; private set; }
        internal byte[] DocBytes { get; private set; }

        internal byte[] PullBytes { get; private set; }

        internal bool IsDownloadCompleted
        {
            get
            {
                return PullBytes != null;
                //return LibBytes != null && DocBytes != null;
            }
        }

        internal bool IsDenied { get; private set; }

        internal ClientDownload(string communicationID, string username, string password)
        {
            _communicationID = communicationID;
            _username = username;
            _password = password;
        }

        internal void FastPull()
        {
            using (var client = new WebClient())
            {
                var parms = new NameValueCollection();
                LibBytes = client.UploadValues(GlobalResources.ApiDownloadLib, "POST", parms);
                DocBytes = client.UploadValues(GlobalResources.ApiDownloadDoc, "POST", parms);
            }
        }

        internal byte[] PullZip()
        {
            using (var client = new WebClient())
            {
                var parms = new NameValueCollection();
                PullBytes = client.UploadValues(GlobalResources.ApiDownloadZip, "POST", parms);
                return PullBytes;
            }
        }

        #region DEPRECATED

        /*
        // >= 2000ms
        internal void Pull()
        {
            string decryptedSimKey;
            CryptographyManager cryptographyManager;

            // lib:
            var xmlMessageLib = CommunicationManager.PrepareCommand(_communicationID, Commands.PULLLIB, _username, _password,
                out decryptedSimKey, out cryptographyManager);
            LibBytes = PullLib(xmlMessageLib);
            if (IsDenied)
            {
                return;
            }

            // doc:
            var xmlMessageDoc = CommunicationManager.PrepareCommand(_communicationID, Commands.PULLLIBDOC, _username, _password,
                out decryptedSimKey, out cryptographyManager);
            DocBytes = PullDoc(xmlMessageDoc);
        }

        private byte[] PullLib(string xmlMessage)
        {
            string response = CommunicationManager.SendXmlMessage(xmlMessage, GlobalResources.PullUrl);
            if (string.IsNullOrEmpty(response))
            {
                IsDenied = true;
                return null;
            }

            if (CommunicationManager.IsDenied(response))
            {
                IsDenied = true;
                return null;
            }

            using (var client = new WebClient())
            {
                return client.DownloadData(Path.Combine(GlobalResources.WebDownloadPath, GlobalResources.LibraryFile));
            }
        }

        private byte[] PullDoc(string xmlMessage)
        {
            string response = CommunicationManager.SendXmlMessage(xmlMessage, GlobalResources.PullUrl);
            if (string.IsNullOrEmpty(response))
            {
                IsDenied = true;
                return null;
            }

            if (CommunicationManager.IsDenied(response))
            {
                IsDenied = true;
                return null;
            }

            using (var client = new WebClient())
            {
                return client.DownloadData(Path.Combine(GlobalResources.WebDownloadPath, GlobalResources.DocumentationFile));
            }
        }
        */

        #endregion

    }
}