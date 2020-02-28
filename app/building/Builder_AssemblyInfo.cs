using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.CodeDom.Compiler;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using QueryTalk.Wall;

namespace QueryTalk.Mapper
{
    internal partial class Builder
    {

        private void AppendAssemblyInfo()
        {
            // assembly version
            _csharp
                .AppendFormatLine(@"[assembly: System.Reflection.AssemblyVersion(""{0}.{1}.{2}.{3}"")]",
                    _mappingHandler.AssemblyVersion.Item1,
                    _mappingHandler.AssemblyVersion.Item2,
                    _mappingHandler.AssemblyVersion.Item3,
                    _mappingHandler.AssemblyVersion.Item4);

            // file version
            _csharp
                .AppendFormatLine(@"[assembly: System.Reflection.AssemblyFileVersion(""{0}.{1}.{2}.{3}"")]",
                    _mappingHandler.FileVersion.Item1,
                    _mappingHandler.FileVersion.Item2,
                    _mappingHandler.FileVersion.Item3,
                    _mappingHandler.FileVersion.Item4);

            // other
            _csharp.AppendLine(@"[assembly: System.Reflection.AssemblyProduct(""QueryTalk.Db"")]");
            _csharp.AppendFormatLine(@"[assembly: System.Reflection.AssemblyCopyright(""QueryTalk © {0}"")]", DateTime.Now.Year);
            _csharp.AppendLine(@"[assembly: System.Reflection.AssemblyTrademark(""QueryTalk"")]");

            // signing
            if (_mappingHandler.KeyFile != null && _mappingHandler.KeyFile.IsDelaySigning)
            {
                _csharp.AppendLine(@"[assembly: System.Reflection.AssemblyDelaySignAttribute(true)]");
            }

            // sign assembly
            //_csharp.AppendLine(@"[assembly:AssemblyKeyFileAttribute(""C:\\AMOS\\SVN\\QUERYTALK\\QueryTalk 1.0\\QueryTalk\\GetSolidSoft.snk"")]");
            //_csharp.AppendLine(@"[assembly:AssemblyKeyFileAttribute(""C:\\AMOS\\SVN\\QUERYTALK\\QueryTalk 1.0\\QueryTalk.App\\QueryTalk.Mapper\\Mapper.pfx"")]");
        }

        // convert PFX to SNK
        public void ConvertPfxToSnk()
        {
            System.Exception ex;
            var cert = Common.TryCreateCertificate(_mappingHandler.KeyFile, out ex);
            if (ex != null)
            {
                throw ex;
            }

            // create .snk
            var privateKey = (System.Security.Cryptography.RSACryptoServiceProvider)cert.PrivateKey;

            // save .snk
            _mappingHandler.KeyFile.TempSnkFileName = String.Format("{0}.snk", Regex.Replace(Guid.NewGuid().ToString(), "-", ""));
            using (FileStream fs = File.Open(Path.Combine(Directory.GetCurrentDirectory(), _mappingHandler.KeyFile.TempSnkFileName.ToString()), 
                FileMode.Create))
            {
                var snkData = privateKey.ExportCspBlob(true);
                fs.Write(snkData, 0, snkData.Length);
                fs.Close();
            }
        }

    }
}
