namespace PageObjects.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task DownloadFile(this HttpClient httpClient, Uri fileUri, string pathToDownload)
        {
            var fileName = Path.GetFileName(fileUri.LocalPath);

            var httpResult = await httpClient.GetAsync(fileUri);
            using var resultStream = await httpResult.Content.ReadAsStreamAsync();

            using var fileStream = File.Create(Path.Combine(pathToDownload, fileName));
            resultStream.CopyTo(fileStream);
        }
    }
}
