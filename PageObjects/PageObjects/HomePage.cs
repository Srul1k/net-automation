using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace PageObjects.PageObjects
{
    public class HomePage
    {
        private IWebDriver _driver;

        [FindsBy(How = How.XPath, Using = "//a[@class='top-navigation__item-link' and text()='Careers']")]
        public IWebElement CareersLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[@class='top-navigation__item-link' and text()='About']")]
        public IWebElement AboutLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[@class='top-navigation__item-link' and text()='Insights']")]
        public IWebElement InsightsLink { get; set; }

        [FindsBy(How = How.Id, Using = "onetrust-accept-btn-handler")]
        public IWebElement AcceptCookiesButton { get; set; }

        [FindsBy(How = How.CssSelector, Using = "button.header-search__button")]
        public IWebElement MagnifierButton { get; set; }

        [FindsBy(How = How.Id, Using = "new_form_search")]
        public IWebElement SearchInput { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@class='header-search__submit']")]
        public IWebElement FindButton { get; set; }

        public HomePage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
        }
    }
}
