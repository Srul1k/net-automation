using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System.Collections.ObjectModel;

namespace PageObjects.PageObjects
{
    public class SearchPage
    {
        private IWebDriver _driver;

        [FindsBy(How = How.ClassName, Using = "search-results__view-more")]
        public IWebElement ViewMoreLink { get; set; }

        public SearchPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
        }

        public ReadOnlyCollection<IWebElement> GetArticles()
        {
            return _driver.FindElements(By.XPath("//article[@class='search-results__item']"));
        }
    }
}