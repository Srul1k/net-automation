using OpenQA.Selenium;

namespace Core.Core
{
    public static class DriverHolder
    {
        [ThreadStatic]
        private static IWebDriver _instance;

        public static IWebDriver Driver
        {
            get
            {
                if (_instance is null)
                {
                    InitDriver(new DriverSettings() { BrowserName = BrowserName.Chrome, RemoteMode = true });
                }

                return _instance;
            }
        }

        public static void InitDriver(DriverSettings driverSettings)
        {
            _instance = BrowserFactory.GetBrowser(driverSettings);
        }

        public static void Cleanup()
        {
            _instance?.Quit();
            _instance = null;
        }
    }
}
