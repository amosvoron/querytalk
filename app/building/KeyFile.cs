using System.IO;

namespace QueryTalk.Mapper
{
    internal class KeyFile
    {
        internal string FilePath { get; set; }          // PFX or SNK file 
        internal string SnkFilePath                     // the file path of the target SNK (either given or created by PFX file)
        {
            get
            {
                if (IsPfx)
                {
                    return Path.Combine(Directory.GetCurrentDirectory(), TempSnkFileName);
                }
                else
                {
                    return FilePath;
                }
            }
        }
        internal bool IsPfx                             // true if PFX
        {
            get
            {
                if (FilePath != null)
                {
                    return System.IO.Path.GetExtension(FilePath).ToLower() == ".pfx";
                }

                return false;
            }
        }
        internal string PfxPassword { get; set; }       // PFX file password
        internal bool IsDelaySigning { get; set; }      // is Delay Signing
        internal string TempSnkFileName { get; set; }   // temp SNK file name created from PFX 

        internal KeyFile()
        { }

        // delete temp .snk
        internal void ClearTemp()
        {
            try
            {
                if (IsPfx)
                {
                    File.Delete(Path.Combine(Directory.GetCurrentDirectory(), TempSnkFileName));
                }
            }
            catch { }  // do nothing
        }
    }
}
