using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;
using QueryTalk.Security;

namespace QueryTalk.Mapper
{
    internal static class Log
    {
        internal static void LogUse(int useTypeCode, string data = null)
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    using (WebClient client = new WebClient())
                    {
                        var reqParams = new NameValueCollection();
                        reqParams.Add("communicationID", Uri.EscapeDataString(Program.License.CommunicationID));
                        reqParams.Add("machineKey", Uri.EscapeDataString(MachineManager.MachineKey));
                        reqParams.Add("useTypeCode", useTypeCode.ToString());
                        reqParams.Add("data", data != null ? Uri.EscapeDataString(data) : data);
                        var response = client.UploadValues(Globals.GlobalResources.LogUseUrl, "POST", reqParams);
                        string responseString = System.Text.Encoding.UTF8.GetString(response);
                    }
                }
                catch
                { }
            });
        }

        internal static void LogNotify(string message)
        {
            LogUse(26, message);
        }

        internal static void LogNotify(string title, string message)
        {
            LogUse(26, string.Format("{0}: {1}", title, message));
        }

        internal static void LogError(string message, string stackTrace)
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    using (WebClient client = new WebClient())
                    {
                        if (stackTrace != null)
                        {
                            if (stackTrace.Length > 4000)
                            {
                                stackTrace = stackTrace.Substring(0, 4000);
                            }
                        }

                        var reqParams = new NameValueCollection();
                        reqParams.Add("message", Uri.EscapeDataString(message));
                        reqParams.Add("stackTrace", Uri.EscapeDataString(stackTrace));
                        var response = client.UploadValues(Globals.GlobalResources.AppErrorUrl, "POST", reqParams);
                        string responseString = System.Text.Encoding.UTF8.GetString(response);
                    }
                }
                catch
                { }
            });

            Thread.Sleep(500);
        }

    }
}
