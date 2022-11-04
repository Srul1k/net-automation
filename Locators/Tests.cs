using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Locators
{
    public class Tests
    {
        private ChromeDriver _driver;

        [SetUp]
        public void StartBrowser()
        {
            var options = new ChromeOptions { PageLoadStrategy = PageLoadStrategy.Normal };
            options.AddArgument("no-sandbox");

            _driver = new ChromeDriver(options);
            _driver.Manage().Window.Maximize();
            _driver.Url = "https://www.epam.com";

            _driver.FindElement(By.Id("onetrust-accept-btn-handler")).Click();
        }

        [TearDown]
        public void CloseBrowser()
        {
            _driver.Quit();
        }

        [TestCase(".NET", "All Locations")]
        public void Test1(string language, string location)
        {
            // Select Carriers link
            _driver.FindElement(By.XPath("//a[@class='top-navigation__item-link' and text()='Careers']")).Click();

            // Input language
            var keywordInput = _driver.FindElement(By.XPath("//input[@placeholder='Keyword']"));
            keywordInput.SendKeys(language);

            // Select location
            _driver.FindElement(By.CssSelector("b[role]")).Click();
            SelectLocation(location);

            // Select Remote option
            _driver.FindElement(By.XPath("//input[@name='remote']/parent::p")).Click();

            // Click on Find button
            _driver.FindElement(By.XPath("//button[@type='submit' and text()='Find']")).Click();

            // Show all results
            ShowAllResults(By.ClassName("search-result__view-more"));

            // Select latest position
            var lastPosition = _driver.FindElement(By.XPath("(//a[@class='search-result__item-apply'])[last()]"));
            _driver.MoveToElement(lastPosition);
            lastPosition.Click();

            // Assert
            Assert.IsTrue(_driver.PageSource.Contains(language));
        }

        [TestCase("BLOCKCHAIN")]
        [TestCase("Cloud")]
        [TestCase("Automation")]
        public void Test2(string searchQuery)
        {
            // Set Implicit wait
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);

            // Click on magnifier icon 
            _driver.FindElement(By.CssSelector("button.header-search__button")).Click();

            // Input search query
            var searchInput = _driver.FindElement(By.Id("new_form_search"));
            searchInput.SendKeys(searchQuery);

            // Click on Find button
            _driver.FindElement(By.XPath("//button[@class='header-search__submit']")).Click();

            // Show all results
            ShowAllResults(By.ClassName("search-results__view-more"));

            // Select all results
            var articles = _driver.FindElements(By.XPath("//article[@class='search-results__item']"));

            // Assert
            Assert.IsTrue(articles.Any(a => a.Text.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)));
        }

        private void ShowAllResults(By viewLocator)
        {
            var viewElement = _driver.FindElement(viewLocator);

            long position = 0;
            while (!viewElement.Displayed)
            {
                var newPosition = _driver.ScrollDown();
                if (position == newPosition)
                {
                    break;
                }

                position = newPosition;
                Thread.Sleep(2000);
            }

            while (viewElement.Displayed)
            {
                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
                wait.Until(ExpectedConditions.ElementIsVisible(viewLocator));

                _driver.MoveToElement(viewElement);
                viewElement.Click();

                Thread.Sleep(3000);
            }
        }

        private void SelectLocation(string location)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            if (location.CompareTo("All Locations") != 0)
            {
                var locationLocator = By.XPath($"//li[text()='{location}']");
                wait.Until(ExpectedConditions.ElementToBeClickable(locationLocator));
                var locationElement = _driver.FindElement(locationLocator);

                locationElement.FindElement(By.XPath("../../child::strong")).Click();
                locationElement.Click();
            }
            else
            {
                var locationLocator = By.XPath($"//li[@title='{location}']");
                wait.Until(ExpectedConditions.ElementToBeClickable(locationLocator));

                _driver.FindElement(locationLocator).Click();
            }
        }
    }
}