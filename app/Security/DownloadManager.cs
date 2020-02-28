using System;
using System.Net;
using QueryTalk.Globals;
using System.IO;

namespace QueryTalk.Security
{

    internal class DownloadManagerWebClient
    {
        internal event DownloadDataCompletedEventHandler DownloadFileToMemoryCompleted;
        internal void DownloadFileToMemory(string remoteUri)
        {
            WebClient DownloadManagerWebClient = new WebClient();
            DownloadManagerWebClient.DownloadDataCompleted += DownloadManagerWebClient_DownloadDataCompleted;
            DownloadManagerWebClient.DownloadDataAsync(new Uri(remoteUri));
        }

        private void DownloadManagerWebClient_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            DownloadFileToMemoryCompleted?.Invoke(sender, e);
        }
    }

    internal static class DownloadManagerWebRequest
    {
        internal static void PullLibResponeAsync(IAsyncResult asyncResult)
        {
            PlayResponeAsync(asyncResult, GlobalResources.LibraryFile);
            //PlayResponeAsync(asyncResult, GlobalResources.LibraryPackage + GlobalResources.LibraryPackageDefaultExtension);
        }

        internal static void PullLibDocResponeAsync(IAsyncResult asyncResult)
        {
            PlayResponeAsync(asyncResult, GlobalResources.DocumentationFile);
            //PlayResponeAsync(asyncResult, GlobalResources.LibraryDocumentation + GlobalResources.LibraryDocumentationDefaultExtension);
        }

        // not implemented:
        //internal static void PullMapperResponeAsync(IAsyncResult asyncResult)
        //{
        //    PlayResponeAsync(asyncResult, GlobalResources.MapperPackage + GlobalResources.MapperPackageDefaultExtension);
        //}

        private static void PlayResponeAsync(IAsyncResult asyncResult, string fileName)
        {
            long total = 0;
            int received = 0;
            HttpWebRequest webRequest = (HttpWebRequest)asyncResult.AsyncState;

            CryptographyManager objQueryTalkCryptography = new CryptographyManager();
            string decryptedsimkey = objQueryTalkCryptography.DecryptAsim(CryptoContext.CryptedSimKey, CryptoContext.RSAPrivateKey);

            try
            {
                using (HttpWebResponse webResponse = (HttpWebResponse)webRequest.EndGetResponse(asyncResult))
                {
                    byte[] buffer = new byte[1024];

                    FileStream fileStream = File.OpenWrite(fileName);
                    using (Stream input = webResponse.GetResponseStream())
                    {
                        total = input.Length;

                        int size = input.Read(buffer, 0, buffer.Length);
                        while (size > 0)
                        {
                            fileStream.Write(buffer, 0, size);
                            received += size;

                            size = input.Read(buffer, 0, buffer.Length);
                        }
                    }

                    fileStream.Flush();
                    fileStream.Close();
                }
            }
            catch (Exception ex)
            {
                Exception ex1 = new Exception(GlobalResources.PullFailedMessage, ex);
            }
        }
    }
}
