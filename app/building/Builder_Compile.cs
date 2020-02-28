using System;
using System.Text;
using System.CodeDom.Compiler;
using System.IO;

namespace QueryTalk.Mapper
{
    internal partial class Builder
    {

        // compile method
        internal void Compile()
        {
            // csharp code as string
            var csharp = _csharp.ToString();

            // references
            CodeDomProvider codeProvider = CodeDomProvider.CreateProvider("CSharp");
            string fileName;

            if (Program.License.IsLimited)
            {
                fileName = String.Format("{0}.{1}-LIMITED.dll", Common.MapperNamespace, _mappingHandler.DbName);
            }
            else
            {
                fileName = String.Format("{0}.{1}.dll", Common.MapperNamespace, _mappingHandler.DbName);
            }

            var parameters = new CompilerParameters();
            parameters.GenerateExecutable = false;
            parameters.OutputAssembly = System.IO.Path.Combine(_repositoryPath, fileName);
            parameters.ReferencedAssemblies.Add(Path.Combine(_mappingHandler.NET_PATH, "System.dll"));
            parameters.ReferencedAssemblies.Add(Path.Combine(_mappingHandler.NET_PATH, "System.Core.dll"));
            parameters.ReferencedAssemblies.Add(Path.Combine(_mappingHandler.NET_PATH, "System.Data.dll"));
            parameters.ReferencedAssemblies.Add(Path.Combine(_mappingHandler.NET_PATH, "System.Xml.dll"));
            parameters.ReferencedAssemblies.Add(Path.Combine(_mappingHandler.NET_PATH, "System.Runtime.Serialization.dll"));
            parameters.ReferencedAssemblies.Add("QueryTalk.dll");

            // assembly sigining
            if (_mappingHandler.KeyFile != null)
            {
                // .pfx
                if (_mappingHandler.KeyFile.IsPfx)
                {
                    ConvertPfxToSnk();
                    parameters.CompilerOptions = String.Format(@"/keyfile:""{0}""", _mappingHandler.KeyFile.TempSnkFileName);
                }
                // .snk
                else
                {
                    parameters.CompilerOptions = String.Format(@"/keyfile:""{0}""", _mappingHandler.KeyFile.SnkFilePath);
                }
            }

            // compile
            CompilerResults results = codeProvider.CompileAssemblyFromSource(parameters, csharp);

            // clear temp file key
            if (_mappingHandler.KeyFile != null)
            {
                _mappingHandler.KeyFile.ClearTemp();
            }

            // check for exception
            if (results.Errors.Count > 0)
            {
                StringBuilder message = new StringBuilder(500);
                foreach (CompilerError CompErr in results.Errors)
                {
                    message.AppendLine(CompErr.ErrorText);
                }           

                throw new System.Exception(message.ToString());
            }
        }

    }
}
