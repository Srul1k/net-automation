using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;

namespace Core.Core
{
    static class BrowserFactory
    {
        internal static IWebDriver GetBrowser(DriverSettings driverSettings)
        {
            switch ((driverSettings.BrowserName, driverSettings.RemoteMode))
            {
                case (BrowserName.Firefox, false):
                    var firefoxOptions = GetFirefoxOptions();
                    return new FirefoxDriver(firefoxOptions);

                case (BrowserName.Chrome, false):
                    var chromeOptions = GetChromeOptions();
                    chromeOptions.EnableHeadless();
                    return new ChromeDriver(chromeOptions);

                case (BrowserName.Firefox, true):
                    var firefoxRemoteOptions = GetFirefoxOptions();
                    firefoxRemoteOptions.EnableHeadless();
                    return new RemoteWebDriver(firefoxRemoteOptions);

                default:
                case (BrowserName.Chrome, true):
                    var chromeRemoteOptions = GetChromeOptions();
                    chromeRemoteOptions.EnableHeadless();
                    return new RemoteWebDriver(chromeRemoteOptions);
            }
        }

        private static FirefoxOptions GetFirefoxOptions()
        {
            var options = new FirefoxOptions() { PageLoadStrategy = PageLoadStrategy.Normal };
            return options;
        }

        private static ChromeOptions GetChromeOptions()
        {
            var options = new ChromeOptions() { PageLoadStrategy = PageLoadStrategy.Normal };
            options.AddArgument("--no-sandbox");
            return options;
        }

        private static void EnableHeadless(this ChromeOptions options)
        {
            options.AddArguments("--headless", "--window-size=1920,1080");
            options.AddUserProfilePreference("download.default_directory", Environment.GetEnvironmentVariable("USERPROFILE") + "\\Downloads");
        }

        private static void EnableHeadless(this FirefoxOptions options)
        {
            options.AddArguments("--headless", "--window-size=1920,1080");
        }
    }
}
