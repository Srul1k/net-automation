using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace PageObjects.Extensions
{
    public static class DriverExtensions
    {
        public static void MoveToElement(this IWebDriver driver, IWebElement element)
        {
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
    }
}
