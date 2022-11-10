using Core.Core;
using OpenQA.Selenium;

namespace Tests.Utils
{
    public static class ScreenshotMaker
    {
        private static string NewScreenshotName
        {
            get { return "screenshot-" + DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss-fff") + "." + ScreenshotImageFormat; }
        }

        private static ScreenshotImageFormat ScreenshotImageFormat
        {
            get { return ScreenshotImageFormat.Png; }
        }

        public static void TakeScreenshot()
        {
            var folderPath = Path.Combine(Environment.CurrentDirectory, "screenshots");
            Directory.CreateDirectory(folderPath);

            var screenshotPath = Path.Combine(folderPath, NewScreenshotName);
            var image = ((ITakesScreenshot)DriverHolder.Driver).GetScreenshot();
            image.SaveAsFile(screenshotPath, ScreenshotImageFormat);
        }
    }
}