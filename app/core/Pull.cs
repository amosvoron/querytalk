using System;
using System.Text;
using System.Reflection;
using System.Text.RegularExpressions;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;
using System.Threading;
using System.Net;
using Newtonsoft.Json;
using QueryTalk.Security;
using QueryTalk.Globals;

namespace QueryTalk.Mapper
{
    internal class Pull
    {
        // invokes handler when all pull actiones terminate (with denial, success, cancellation, or failure)
        internal event EventHandler PullTerminated;

        private CancellationTokenSource _taskToken;

        // la descarga ha sido rechazada, cancelada o fallida => no se puede guardar
        private bool _failed;
        internal bool Success
        {
            get
            {
                return !_failed;
            }
        }

        private static Start _start
        {
            get
            {
                return Program.Start;
            }
        }

        private bool IsTaskCanceled
        {
            get
            {
                return _taskToken != null && _taskToken.IsCancellationRequested;
            }
        }

        private static string _zipPath
        {
            get
            {
                return Path.Combine(_start.RepositoryPath, GlobalResources.ZipFile);
            }
        }

        private static string _libraryPath
        {
            get
            {
                return Path.Combine(_start.RepositoryPath, GlobalResources.LibraryFile);
            }
        }

        internal static bool LibraryExists
        {
            get
            {
                return File.Exists(_libraryPath);
            }
        }

        private static string _documentationPath
        {
            get
            {
                return Path.Combine(_start.RepositoryPath, GlobalResources.DocumentationFile);
            }
        }

        internal Pull(CancellationTokenSource taskToken)
        {
            _taskToken = taskToken;
        }

        #region Execute Pull

        // forced pull
        internal bool ForcePull()
        {
            var download = new ClientDownload(new Guid().ToString("D"), Program.License.User, Program.License.Password);

            //download.FastPull();
            var bytes = download.PullZip();
            if (bytes == null || bytes.Length == 0)
            {
                throw new Exception("Pull failed. Try later.");
            }

            Program.Start.PullBytes = bytes;

            if (IsTaskCanceled)
            {
                _failed = true;
                _TerminatePull(download);
                return false;
            }

            if (download.IsDenied)
            {
                _failed = true;
                Common.ShowNotification("Pull has been denied.");
                _TerminatePull(download);
                return false;
            }

            return _TerminatePull(download);
        }

        // execute auto pull (on repository path changed)
        internal bool TryPull(bool fast)
        {
            if (!LibraryExists)
            {
                return ForcePull();
            }

            return false;
        }

        // execute on total completion - when download of both files is finished with success
        private bool _TerminatePull(ClientDownload download)
        {
            var saved = false;

            if (!_failed && download.IsDownloadCompleted)
            {
                Extract(download.PullBytes);
                saved = true;
            }

            PullTerminated?.Invoke(this, new EventArgs());
            return saved;
        }

        internal void Extract(byte[] bytes)
        {
            File.WriteAllBytes(_zipPath, bytes);
            _Extract();
        }

        private void _Extract()
        {
            // remove all files to be extracted (otherwise an exception is thrown)
            File.Delete(Path.Combine(_start.RepositoryPath, GlobalResources.LibraryFile));
            File.Delete(Path.Combine(_start.RepositoryPath, GlobalResources.DocumentationFile));
            File.Delete(Path.Combine(_start.RepositoryPath, GlobalResources.MapperFile));

            var demoFolder = Path.Combine(_start.RepositoryPath, GlobalResources.QueryTalkDemoFolder);
            if (Directory.Exists(demoFolder))
            {
                Directory.Delete(Path.Combine(_start.RepositoryPath, GlobalResources.QueryTalkDemoFolder), true);
            }

            var zipPath = Path.Combine(_start.RepositoryPath, GlobalResources.ZipFile);
            ZipFile.ExtractToDirectory(zipPath, _start.RepositoryPath);

            // remove zip file
            File.Delete(zipPath);
        }

        #endregion

        #region Versions

        // get the last server version 
        internal Version GetServerVersion(out Exception exception)
        {
            exception = null;

            try
            {
                using (var client = new WebClient())
                {
                    var data = new System.Collections.Specialized.NameValueCollection();
                    client.Encoding = Encoding.UTF8;
                    byte[] response = client.UploadValues(Globals.GlobalResources.ApiGetLastVersionUrl, "POST", data);
                    var json = Encoding.UTF8.GetString(response);
                    return JsonConvert.DeserializeObject<Version>(json);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                return Version.Default;
            }         
        }

        internal static string GetClientLibVersion()
        {
            if (LibraryExists)
            {
                return FileVersionInfo.GetVersionInfo(_libraryPath).FileVersion;
            }
            else
            {
                return Common.NoFile;
            }
        }

        internal static string GetClientAppVersion()
        {
            return Common.GetAssemblyVersion(Assembly.GetEntryAssembly());
        }

        // compare two versions returning:
        //   0: are equal
        //   1: version1 > version2
        //  -1: version1 < version2
        //  -2: incorrect version format
        internal static int CompareVersion(string version1, string version2)
        {
            if (string.IsNullOrWhiteSpace(version1) || string.IsNullOrWhiteSpace(version2))
            {
                return -2;
            }

            if (version1 == Common.NoFile && version2 == Common.NoFile)
            {
                return -2;
            }

            if (version1 == Common.NoFile)
            {
                return -1;
            }

            if (version2 == Common.NoFile)
            {
                return 1;
            }

            // remove spaces, if any
            var ver1 = Regex.Replace(version1, @"\s+", "");
            var ver2 = Regex.Replace(version2, @"\s+", "");

            // split
            var vv1 = ver1.Split(new char[] { '.' });
            var vv2 = ver2.Split(new char[] { '.' });

            for (int i = 0; i < vv1.Length && i < vv2.Length; ++i)
            {
                var v1 = Regex.Replace(vv1[i], @"[^\d]", "");
                var v2 = Regex.Replace(vv2[i], @"[^\d]", "");

                if (string.IsNullOrWhiteSpace(v1) || string.IsNullOrWhiteSpace(v2))
                {
                    return -2;
                }

                int digit1, digit2;
                if (int.TryParse(v1, out digit1))
                {
                    if (int.TryParse(v2, out digit2))
                    {
                        if (digit1 == digit2)
                        {
                            continue;
                        }

                        // greater than
                        if (digit1 > digit2)
                        {
                            return 1;
                        }

                        // less than
                        return -1;
                    }
                    else
                    {
                        return -2;
                    }
                }
                else
                {
                    return -2;
                }
            }

            return 0;
        }

        #endregion

    }
}
