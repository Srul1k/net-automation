using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace Core.Core
{
    class BrowserFactory
    {
        internal static IWebDriver GetBrowser(BrowserName browserName)
        {
            switch (browserName)
            {
                case BrowserName.Firefox:
                    return new FirefoxDriver();

                case BrowserName.InternetExplorer:
                    return new InternetExplorerDriver();

                case BrowserName.Chrome:
                    var options = new ChromeOptions { PageLoadStrategy = PageLoadStrategy.Normal };
                    options.AddArgument("no-sandbox");
                    return new ChromeDriver(options);

                default:
                    throw new ArgumentException("Browser name is not valid");
            }
        }
    }
}
