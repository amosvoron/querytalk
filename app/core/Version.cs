namespace QueryTalk.Mapper
{
    public class Version
    {
        public string LibVersion { get; set; }

        public string LibDescription { get; set; }

        public string AppVersion { get; set; }

        public string AppDescription { get; set; }

        internal static Version Default
        {
            get
            {
                return new Version()
                {
                    LibVersion = "0.0.0",
                    AppVersion = "0.0.0",
                    LibDescription = "",
                    AppDescription = ""
                };
            }
        }

        internal int CompareToLibVersion(string version)
        {
            return Pull.CompareVersion(LibVersion, version);
        }

        internal int CompareToAppVersion(string version)
        {
            return Pull.CompareVersion(AppVersion, version);
        }

    }
}
