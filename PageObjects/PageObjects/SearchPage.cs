using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PageObjects.Extensions;
using PageObjects.PageObjects.Interfaces;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System.Collections.ObjectModel;

namespace PageObjects.PageObjects
{
    public class SearchPage : IResultsPage
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

        public void ShowAllResults()
        {
            long position = 0;
            while (!ViewMoreLink.Displayed)
            {
                var newPosition = _driver.ScrollDown();
                if (position == newPosition)
                {
                    break;
                }

                position = newPosition;
                Thread.Sleep(2000);
            }

            while (ViewMoreLink.Displayed)
            {
                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
                wait.Until(ExpectedConditions.ElementIsVisible
                    (By.ClassName(ViewMoreLink.GetAttribute("class"))));

                _driver.MoveToElement(ViewMoreLink);
                ViewMoreLink.Click();

                Thread.Sleep(3000);
            }
        }
    }
}