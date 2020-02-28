using System;
using System.Net;
using QueryTalk.Globals;

namespace QueryTalk.Security
{
    internal delegate void UpdateCompleted(UpdateResult result);

    internal enum UpdateResult { Completed = 0, Canceled = 1, Error = 2 }

    internal static class UpdateManager
    {
        internal static event UpdateCompleted UpdateQueryTalkerCompletedEventHandler;
        internal static event UpdateCompleted UpdateQueryTalkCompletedEventHandler;
        internal static bool UpdatingQueryTalker { get; set; }
        internal static bool UpdatingQueryTalk { get; set; }

        internal static void UpdateQueryTalker()
        {
            string myStringWebResource = GlobalResources.WebDownloadPath + GlobalResources.MapperPackage;

            WebClient myWebClient = new WebClient();
            myWebClient.DownloadDataCompleted += MyWebClient_DownloadQueryTalkerCompleted;

            myWebClient.DownloadFileAsync(new Uri(myStringWebResource), GlobalResources.MapperPackage);
        }

        private static void MyWebClient_DownloadQueryTalkerCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                UpdateQueryTalkerCompletedEventHandler?.Invoke(UpdateResult.Completed);
            }
            if (e.Error != null)
            {
                UpdateQueryTalkerCompletedEventHandler?.Invoke(UpdateResult.Error);
            }
            if (e.Cancelled)
            {
                UpdateQueryTalkerCompletedEventHandler?.Invoke(UpdateResult.Canceled);
            }
        }

        internal static void UpdateQueryTalk()
        {
            string myStringWebResource = GlobalResources.WebDownloadPath + GlobalResources.LibraryPackage;

            WebClient myWebClient = new WebClient();
            myWebClient.DownloadDataCompleted += MyWebClient_DownloadQueryTalkCompleted;

            myWebClient.DownloadFileAsync(new Uri(myStringWebResource), GlobalResources.LibraryPackage);
        }

        private static void MyWebClient_DownloadQueryTalkCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                UpdateQueryTalkerCompletedEventHandler?.Invoke(UpdateResult.Completed);
            }
            if (e.Error != null)
            {
                UpdateQueryTalkerCompletedEventHandler?.Invoke(UpdateResult.Error);
            }
            if (e.Cancelled)
            {
                UpdateQueryTalkerCompletedEventHandler?.Invoke(UpdateResult.Canceled);
            }
        }
    }
}
