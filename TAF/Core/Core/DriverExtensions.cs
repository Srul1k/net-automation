using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Core.Core
{
    public static class DriverExtensions
    {
        public static void MoveToElement(this IWebDriver driver, IWebElement element)
        {
            driver.ScrollToElement(element);
            Actions actions = new Actions(driver);
            actions.MoveToElement(element);
            actions.Perform();
        }

        public static long ScrollDown(this IWebDriver driver)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollBy(0,document.body.scrollHeight)");

            return (long)js.ExecuteScript("return document.body.scrollHeight");
        }

        public static void ScrollToElement(this IWebDriver driver, IWebElement element)
        {
            var x = element.Location.X;
            var y = element.Location.Y;

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript($"window.scrollTo({x},{y})");
            js.ExecuteScript($"window.scrollBy(0, -210)");
        }

        public static void WaitUntilFileIsDownloaded(this IWebDriver driver, string filePath)
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(driver =>
            {
                if (File.Exists(filePath))
                {
                    return true;
                }

                return false;
            });
        }

        public static void ShowAllResults(this IWebDriver driver, By viewMoreLink)
        {
            long position = 0;
            var viewMoreElement = driver.FindElement(viewMoreLink);

            while (!viewMoreElement.Displayed)
            {
                var newPosition = driver.ScrollDown();
                if (position == newPosition)
                {
                    break;
                }

                position = newPosition;
                Thread.Sleep(2000);
            }

            while (viewMoreElement.Displayed)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(ExpectedConditions.ElementIsVisible(viewMoreLink));

                driver.MoveToElement(viewMoreElement);
                viewMoreElement.Click();

                Thread.Sleep(4000);
            }
        }
    }
}
