using Business.ApplicationInterfaces;
using Core.Core;

namespace Business.Business
{
    public class AboutContext
    {
        private AboutPage page = new AboutPage();

        public void DownloadFile(string filePath)
        {
            var downloadLink = DriverHolder.Driver.FindElement(page.DownloadLink);

            DriverHolder.Driver.MoveToElement(downloadLink);
            downloadLink.Click();

            DriverHolder.Driver.WaitUntilFileIsDownloaded(filePath);
        }
    }
}
