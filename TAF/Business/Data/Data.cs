namespace Business.Data
{
    public static class Data
    {
        public const string APIUrl = "https://jsonplaceholder.typicode.com";
        public const string ApplicationUrl = "https://www.epam.com";
        public const string NameOfFileToDownload = "EPAM_Systems_Company_Overview.pdf";
        public static string DownloadFolder = Environment.GetEnvironmentVariable("USERPROFILE") + "\\Downloads";
        public static string FilePath = Path.Combine(DownloadFolder, NameOfFileToDownload);
    }
}
