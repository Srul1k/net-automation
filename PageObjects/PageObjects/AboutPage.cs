using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace PageObjects.PageObjects
{
    public class AboutPage
    {
        private IWebDriver _driver;

        [FindsBy(How = How.XPath, Using = "//a[@download]")]
        public IWebElement DownloadLink { get; set; }

        public AboutPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
        }
    }
}