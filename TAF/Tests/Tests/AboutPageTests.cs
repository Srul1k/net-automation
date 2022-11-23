using Business.Business;
using Business.Data;
using NUnit.Framework;
using Tests.Utils;

namespace Tests.Tests
{
    [Parallelizable(scope: ParallelScope.All)]
    public class AboutPageTests : TestsSetup
    {
        [Test]
        public void Validate_FileDownloadFunction_WorksAsExpected()
        {
            try
            {
                LoggerHolder.Logger.Debug($"Run test {nameof(Validate_FileDownloadFunction_WorksAsExpected)}");

                var home = new HomeContext();

                LoggerHolder.Logger.Info("Open About page");
                var about = home.OpenAboutPage();

                LoggerHolder.Logger.Info("Download file");
                about.DownloadFile(Data.FilePath);

                LoggerHolder.Logger.Info("Check is file downloaded");
                Assert.IsTrue(File.Exists(Data.FilePath));
            }
            catch (Exception ex)
            {
                LoggerHolder.Logger.Fatal(ex);
                ScreenshotMaker.TakeScreenshot();
                throw;
            }
        }
    }
}
