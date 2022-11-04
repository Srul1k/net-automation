using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace PageObjects.PageObjects
{
    public class CareersPage
    {
        private IWebDriver _driver;

        [FindsBy(How = How.XPath, Using = "//input[@placeholder='Keyword']")]
        public IWebElement KeywordInput { get; set; }

        [FindsBy(How = How.CssSelector, Using = "b[role]")]
        public IWebElement LocationUnwrapper { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@name='remote']/parent::p")]
        public IWebElement RemoteOption { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@type='submit' and text()='Find']")]
        public IWebElement FindButton { get; set; }

        [FindsBy(How = How.ClassName, Using = "search-result__view-more")]
        public IWebElement ViewMoreLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//li[@title='All Locations']")]
        public IWebElement AllLocationsItem { get; set; }

        [FindsBy(How = How.XPath, Using = "(//a[@class='search-result__item-apply'])[last()]")]
        public IWebElement LatestPositionViewAndApplyButton { get; set; }

        public CareersPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
        }

        public IWebElement GetLocationItem(string location)
        {
            return _driver.FindElement(By.XPath($"//li[text()='{location}']"));
        }

        public IWebElement GetParentLocationItem(string location)
        {
            return GetLocationItem(location).FindElement(By.XPath("../../child::strong"));
        }
    }
}
