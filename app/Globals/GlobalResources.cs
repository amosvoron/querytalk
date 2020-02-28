namespace QueryTalk.Globals
{
    internal static class GlobalResources
    {
        internal static readonly string QueryTalkUrlDomain = "https://querytalk.com";
        // internal static readonly string QueryTalkUrlDomain = "https://localhost:44300";

        internal static bool IsLocalhost
        {
            get
            {
                return System.Text.RegularExpressions.Regex.IsMatch(QueryTalkUrlDomain, "(localhost)");
            }
        }

        #region App links

        internal static readonly string AppLicenseUrl = QueryTalkUrlDomain + "/Account/MyLicense";
        internal static readonly string AppEulaUrl = QueryTalkUrlDomain + "/Home/Eula";
        //internal static readonly string AppLimitedVersionUrl = QueryTalkUrlDomain + "/mapper-limited-version";  // deprecated
        internal static readonly string AppLicenseVerificationFailedUrl = QueryTalkUrlDomain + "/Home/LicenseVerificationFailed";
        //internal static readonly string AppHelpUrl = QueryTalkUrlDomain + "/mapper-help";
        internal static readonly string AppDownloadUrl = QueryTalkUrlDomain + "/download";
        internal static readonly string ApiGetLastVersionUrl = QueryTalkUrlDomain + "/api/GetLastVersion";
        internal static readonly string AppContactUrl = QueryTalkUrlDomain + "/contacts";
        internal static readonly string ApiDownloadLib = QueryTalkUrlDomain + "/Api/DownloadLib";
        internal static readonly string ApiDownloadDoc = QueryTalkUrlDomain + "/Api/DownloadDoc";
        internal static readonly string ApiDownloadZip = QueryTalkUrlDomain + "/Api/DownloadZip";
        internal static readonly string AppRegisterUrl = QueryTalkUrlDomain + "/Account/Register";
        internal static readonly string RegisterAnonymousEmailUrl = QueryTalkUrlDomain + "/Api/RegisterAnonymousEmail";
        internal static readonly string AppMapDatabaseVideo = QueryTalkUrlDomain + "/Video/MapDatabase";
        internal static readonly string AppRepositoryVideo = QueryTalkUrlDomain + "/Video/Repository";
        internal static readonly string AppPullVideo = QueryTalkUrlDomain + "/Video/Pull";
        internal static readonly string AppSetServerVideo = QueryTalkUrlDomain + "/Video/ConnectToServer";
        internal static readonly string AppQueryTalkBaseVideo = QueryTalkUrlDomain + "/Video/CreateQueryTalkBase";
        internal static readonly string AppDotNetDirectoryVideo = QueryTalkUrlDomain + "/Video/DotNetDirectory";
        internal static readonly string AppMapperSettingsVideo = QueryTalkUrlDomain + "/Video/MapperSettings";
        internal static readonly string AppFreeTrialEmailVideo = QueryTalkUrlDomain + "/Video/FreeTrialEmail";
        internal static readonly string AppExcludedObjectsVideo = QueryTalkUrlDomain + "/Video/ExcludedObjects";

        #endregion

        #region Seguridad, licenciamiento

        internal static readonly string StartCommunicationUrl = QueryTalkUrlDomain + "/Api/Handshaking";
        internal static readonly string LicenseUrl = QueryTalkUrlDomain + "/Api/License";
        public static readonly string LicenseAnonymousUrl = QueryTalkUrlDomain + "/Api/LicenseAnonymous";
        internal static readonly string ViewLicenseUrl = QueryTalkUrlDomain + "/Api/ViewLicense";
        internal static readonly string LogUseUrl = QueryTalkUrlDomain + "/Api/LogUse";
        internal static readonly string AppErrorUrl = QueryTalkUrlDomain + "/Error/AppError";

        internal static readonly string ComunicationKey = "f3671040-f490-4a2d-943b-2c97783a2e4a";

        internal static readonly string Level1 = @"SOFTWARE";
        internal static readonly string Level2 = @"QueryTalk";
        internal static readonly string Level3 = @"Credential";
        internal static readonly string Key1 = @"credential1";
        internal static readonly string Key2 = @"credential2";
        internal static readonly string Key3 = @"credential3";
        internal static readonly string HasAccountKey = @"HasAccount";

        #region Security Messages
        internal static string ServerFailedMessage = "Denied:Server connection failed.";
        internal static string LoginFailedMessage = "Denied:Login failed.";
        internal static string LicenseFailedMessage = "Denied:License verification failed.";
        internal static string TrialPeriodExpiredMessage = "Denied:Trial period expired.";
        #endregion        

        #endregion

        #region Actualizaciones, descargas

        //Entrega la dirección de ip plana sin mas ninguna otra información
        internal static readonly string ExternalRouterIpService = "http://icanhazip.com";

        //Entrega un Json con la información estructurada
        internal static readonly string GeoLocalizationByIpService = "http://ip-api.com/json/";

        internal static readonly string VersionsUrl = QueryTalkUrlDomain + "/Api/GetVersions";
        internal static readonly string PullUrl = QueryTalkUrlDomain + "/Api/Pull";

        internal static readonly string WebDownloadPath = QueryTalkUrlDomain + "/Downloads";
        internal static readonly string FileSystemDownloadPath = "\\Downloads\\";

        internal static readonly string LibraryFile = "QueryTalk.dll";
        internal static readonly string DocumentationFile = "QueryTalk.xml";
        internal static readonly string ApplicationFile = "QueryTalker.exe";
        internal static readonly string MapperFile = "QueryTalk.Db.QueryTalkBase.dll";
        internal static readonly string QueryTalkDemoFolder = "QueryTalkDemo";
        public static readonly string ZipFile = "QueryTalk.zip";

        // obsoleto:

        internal static readonly string FullDownLoadPackage = "FullDownload";
        internal static readonly string FullDownLoadPackageDefaultExtension = ".zip";

        internal static readonly string MapperPackage = "QueryTalker";
        internal static readonly string MapperPackageDefaultExtension = ".zip";

        internal static readonly string LibraryPackage = "QueryTalk";
        internal static readonly string LibraryPackageDefaultExtension = ".zip";

        internal static readonly string LibraryDocumentation = "QueryTalkDocumentation";
        internal static readonly string LibraryDocumentationDefaultExtension = ".xml";

        #region Pull Messages
        internal static string PullFailedMessage = "Denied:Server pull failed.";
        #endregion

        #endregion

        #region Bases de datos y almacenamiento        
        internal static readonly string ActiveConnectionStrings = "SQLServerAzureConnection";

        internal static readonly string WebDocumentPath = "/Documents/";
        #endregion

        #region Enrutamiento dinamico
        internal static readonly string ControllerQueryTalkerHelp = "Tutorials";
        internal static readonly string ActionQueryTalkerHelp = "Index";

        internal static readonly string ControllerMapperHelp = "Tutorials";
        internal static readonly string ActionMapperHelp = "Index";

        internal static readonly string ControllerMapperLimitedVersion = "Tutorials";
        internal static readonly string ActionMapperLimitedVersion = "Index";
        #endregion        
    }
}
