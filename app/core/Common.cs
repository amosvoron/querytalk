using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace QueryTalk.Mapper
{
    internal static class Common
    {
        internal const string NOTIFICATION = "QueryTalker Notification";
        internal const string MapperNamespace = "QueryTalk.Db";
        internal const string NL1 = @"\par ";
        internal const string NL2 = @"\par \par ";
        internal const string RTBegin = @"{\rtf1\ansi ";
        internal const string RTEnd = "}";
        internal const string NoFile = "No file.";
        internal const string SameVersion = "The same as the local version.";
        internal const string NetPath32 = @"C:\Windows\Microsoft.NET\Framework\v4.0.30319";
        internal const string NetPath64 = @"C:\Windows\Microsoft.NET\Framework64\v4.0.30319";
        internal const string QueryTalkBase = "QueryTalkBase";

        internal static bool CheckNetDirectory(string netPath)
        {
            return File.Exists(Path.Combine(netPath, "System.dll"));
        }

        internal static readonly Color Yellow = Color.FromArgb(251, 228, 168);   

        //internal static readonly Color BackgroundColor = Color.FromArgb(220, 234, 239);   // blue
        //internal static readonly Color BackgroundColor = Color.FromArgb(251, 228, 168);     // yellow
        internal static readonly Color BackgroundColor = Color.White;

        internal static string AssemblyVersion
        {
            get
            {
                return (Assembly.GetEntryAssembly()
                    .GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute), false)
                        as AssemblyInformationalVersionAttribute[])[0].InformationalVersion;
            }
        }

        internal static string GetAssemblyVersion(Assembly assembly)
        {
            return (assembly
                .GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute), false)
                    as AssemblyInformationalVersionAttribute[])[0].InformationalVersion;
        }

        // thread safe invoker on Windows.Forms control
        internal static void ThreadSafeInvoke(this System.Windows.Forms.Control ctr, Action action)
        {
            if (ctr.InvokeRequired)
            {
                ctr.Invoke(new Action(action));
            }
            else
            {
                action();
            }
        }

        // compare two strings, ordinal, case-insensitive
        internal static bool EqualIgnoreCase(this string value1, string value2)
        {
            return String.Compare(value1, value2, StringComparison.OrdinalIgnoreCase) == 0;
        }

        // csharp line formatter
        internal static StringBuilder AppendFormatLine(this StringBuilder stringBuilder, string format, params object[] args)
        {
            return stringBuilder.AppendFormat(format, args).AppendLine();
        }

        // escape double quote
        internal static string EscapeDoubleQuote(this string text)
        {
            var text2 = Regex.Replace(text, @"\\", @"\\");  // escape back slash
            return Regex.Replace(text2, "\"", "\\\"");      // escape double quote
        }

        // convert bool value into CLR string representation
        internal static string ToClrString(this bool value)
        {
            return value ? "true" : "false";
        }

        // IsProcessing
        internal static bool IsProcessing(this ProcessState state)
        {
            return state == ProcessState.ProcessingTryConnection
                || state == ProcessState.Processing
                || state == ProcessState.Compiling;
        }

        // is column a RK
        internal static bool IsRK(this ColumnKeyType key)
        {
            return (key == ColumnKeyType.RK || key == ColumnKeyType.RKFK);
        }

        // is column a FK
        internal static bool IsFK(this ColumnKeyType key)
        {
            return (key == ColumnKeyType.FK || key == ColumnKeyType.RKFK);
        }

        // returns true if the direction of the relation is from FK to RK
        internal static bool IsFromFKToRK(this Relation relation)
        {
            return (RelationType)relation.RELATION_TYPE == RelationType.ManyToOne
                || (RelationType)relation.RELATION_TYPE == RelationType.Self
                || (RelationType)relation.RELATION_TYPE == RelationType.SingleToOne;
        }

        // returns true if the direction of the relation is from RK to FK
        internal static bool IsFromRKToFK(this Relation relation)
        {
            return (RelationType)relation.RELATION_TYPE == RelationType.OneToMany
                || (RelationType)relation.RELATION_TYPE == RelationType.OneToSingle;
        }

        // returns true if relation leads from a RK node to many FK nodes (children)
        internal static bool IsOneToMany(this Relation relation)
        {
            return (RelationType)relation.RELATION_TYPE == RelationType.OneToMany;
        }

        // return true if the relation is one-to-single or vice versa
        internal static bool IsSingle(this Relation relation)
        {
            return (RelationType)relation.RELATION_TYPE == RelationType.OneToSingle
                || (RelationType)relation.RELATION_TYPE == RelationType.SingleToOne;
        }

        // try create X509Certificate2 object
        internal static System.Security.Cryptography.X509Certificates.X509Certificate2 TryCreateCertificate(
            KeyFile keyFile, out System.Exception exception)
        {
            exception = null;

            try
            {
                byte[] pfxData;

                // read .pfx
                using (FileStream fs = File.OpenRead(keyFile.FilePath))
                {
                    if (fs.Length == 0)
                    {
                        throw new System.Exception("The key file is empty.");
                    }
                    pfxData = new byte[fs.Length];
                    fs.Read(pfxData, 0, Convert.ToInt32(fs.Length));
                    fs.Close();
                }

                // create certificate
                return new System.Security.Cryptography.X509Certificates.X509Certificate2(
                    pfxData, keyFile.PfxPassword, System.Security.Cryptography.X509Certificates.X509KeyStorageFlags.Exportable);
            }
            catch (System.Exception ex)
            {
                exception = ex;
                return null;
            }
        }

        // is file path valid
        internal static bool IsPathValid(string path)
        {
            // check invalid char
            foreach (var chr in Path.GetInvalidPathChars())
            {
                if (path.Contains(chr))
                {
                    return false;
                }
            }
            return true;
        }

        internal static void SetToolTip(this Control control, string title, string message, int duration = 5)
        {
            var toolTip = new ToolTip();
            toolTip.SetToolTip(control, message);

            if (title != null)
            {
                toolTip.ToolTipTitle = title;
            }

            toolTip.IsBalloon = true;

            toolTip.AutoPopDelay = duration * 1000;
            toolTip.InitialDelay = 300;
            toolTip.ReshowDelay = 300;
            toolTip.ShowAlways = true;
        }

        internal static void SetButton(this Button button)
        {
            button.MouseEnter += (o, e) => { button.BackColor = Color.FromArgb(251, 228, 168); };
            button.MouseLeave += (o, e) => { button.BackColor = Color.Transparent; };
        }

        #region Notify

        internal static void ShowNotification(string message)
        {
            var form = new NotifyForm(message);
            form.ShowDialog();
        }

        internal static void ShowError(Exception ex)
        {
            try
            {
                if (ex != null)
                {
                    var form = new NotifyForm(ex);
                    form.ShowDialog();
                }
                else
                {
                    throw new Exception("Unidentified exception");
                }
            }
            catch (Exception ex2)
            {
                Log.LogError(ex2.Message, ex2.StackTrace);
            }
        }

        internal static void ShowError(Exception ex, MessageType messageType)
        {
            try
            {
                if (messageType == MessageType.MissingObjectsInfo || messageType == MessageType.NoMappingDataInfo)
                {
                    var form = new NotifyForm(messageType);
                    form.ShowDialog();
                }
                else if (ex != null)
                {
                    var form = new NotifyForm(messageType, ex);
                    form.ShowDialog();
                }
                else
                {
                    throw new Exception("Unidentified exception");
                }
            }
            catch (Exception ex2)
            {
                Log.LogError(ex2.Message, ex2.StackTrace);
            }
        }

        internal static void ShowUnknownException(Exception ex)
        {
            try
            {
                if (ex != null)
                {
                    var form = new NotifyForm(MessageType.UnknownError, ex);
                    form.ShowDialog();
                }
                else
                {
                    throw new Exception("Unidentified exception");
                }
            }
            catch (Exception ex2)
            {
                Log.LogError(ex2.Message, ex2.StackTrace);
            }
        }

        internal static void OpenBrowser(string url)
        {
            try
            {
                System.Diagnostics.Process.Start(url);
            }
            catch { }
        }

        internal static void ShowApplicationCrashedException(Exception ex)
        {
            var form = new NotifyForm(MessageType.ApplicationCrashed, ex);
            form.ShowDialog();
        }

        #endregion


    }
}
