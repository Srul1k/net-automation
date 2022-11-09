using OpenQA.Selenium;

namespace Core.Core
{
    public static class DriverHolder
    {
        private static IWebDriver _instance;

        public static IWebDriver Driver
        {
            get
            {
                if (_instance is null)
                {
                    InitDriver(BrowserName.Chrome);
                }

                return _instance;
            }
        }

        public static void InitDriver(BrowserName browserName)
        {
            _instance = BrowserFactory.GetBrowser(browserName);
        }

        public static void Cleanup()
        {
            _instance?.Quit();
            _instance = null;
        }
    }
}
