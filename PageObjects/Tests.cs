using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using PageObjects.Extensions;
using PageObjects.PageObjects;
using SeleniumExtras.WaitHelpers;

namespace PageObjects
{
    public class Tests
    {
        private ChromeDriver _driver;
        private string _nameOfFileToDownload = "EPAM_Systems_Company_Overview.pdf";
        private string _downloadFolder = Environment.GetEnvironmentVariable("USERPROFILE") + "\\Downloads";

        [SetUp]
        public void StartBrowser()
        {
            var options = new ChromeOptions { PageLoadStrategy = PageLoadStrategy.Normal };
            options.AddArgument("no-sandbox");

            _driver = new ChromeDriver(options);
            _driver.Manage().Window.Maximize();
            _driver.Url = "https://www.epam.com";
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            var homePage = new HomePage(_driver);
            homePage.AcceptCookiesButton.Click();
        }

        [TearDown]
        public void CloseBrowser()
        {
            _driver.Quit();
        }

        [TestCase(".NET", "All Locations")]
        public void Validate_SearchForPosition_BasedOnCriteria_WorksAsExpected(string language, string location)
        {
            var homePage = new HomePage(_driver);
            var careersPage = new CareersPage(_driver);

            homePage.CareersLink.Click();
            careersPage.KeywordInput.SendKeys(language);
            careersPage.LocationUnwrapper.Click();

            SelectLocation(location);

            careersPage.RemoteOption.Click();
            careersPage.FindButton.Click();

            ShowAllResults(careersPage.ViewMoreLink);

            _driver.MoveToElement(careersPage.LatestPositionViewAndApplyButton);
            careersPage.LatestPositionViewAndApplyButton.Click();

            Assert.IsTrue(_driver.PageSource.Contains(language));
        }

        [TestCase("BLOCKCHAIN")]
        [TestCase("Cloud")]
        [TestCase("Automation")]
        public void Validate_GlobalSearch_WorksAsExpected(string searchQuery)
        {
            var homePage = new HomePage(_driver);
            var searchPage = new SearchPage(_driver);

            homePage.MagnifierButton.Click();
            homePage.SearchInput.SendKeys(searchQuery);
            homePage.FindButton.Click();

            ShowAllResults(searchPage.ViewMoreLink);

            Assert.IsTrue(searchPage.GetArticles().Any(a => a.Text.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)));
        }

        [Test]
        public void Validate_FileDownloadFunction_WorksAsExpected()
        {
            var homePage = new HomePage(_driver);
            var aboutPage = new AboutPage(_driver);

            homePage.AboutLink.Click();
            _driver.MoveToElement(aboutPage.DownloadLink);

            var uri = new Uri(aboutPage.DownloadLink.GetAttribute("href"));
            var client = new HttpClient();
            client.DownloadFile(uri, _downloadFolder).GetAwaiter().GetResult();

            Assert.IsTrue(CheckFileDownloaded());
        }

        [Test]
        public void Validate_TitleOfArticle_MatchesWith_TitleInCarousel()
        {
            var homePage = new HomePage(_driver);
            var insigthsPage = new InsightsPage(_driver);
            var carouselPage = new CarouselPage(_driver);

            homePage.InsightsLink.Click();

            insigthsPage.GoToThirdElementInCarousel();
            var nameOfArticle = insigthsPage.GetNameOfCarouselArticle();
            insigthsPage.LearnMoreLink.Click();

            Assert.True(carouselPage.NameOfArticle.Text.Contains(nameOfArticle, StringComparison.OrdinalIgnoreCase));
        }

        private void ShowAllResults(IWebElement viewElement)
        {
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
                wait.Until(ExpectedConditions.ElementIsVisible
                    (By.ClassName(viewElement.GetAttribute("class"))));

                _driver.MoveToElement(viewElement);
                viewElement.Click();

                Thread.Sleep(3000);
            }
        }

        private void SelectLocation(string location)
        {
            var careersPage = new CareersPage(_driver);

            if (location.CompareTo("All Locations") != 0)
            {
                careersPage.GetParentLocationItem(location).Click();
                careersPage.GetLocationItem(location).Click();
            }
            else
            {
                careersPage.AllLocationsItem.Click();
            }
        }

        private bool CheckFileDownloaded()
        {
            string[] filePaths = Directory.GetFiles(_downloadFolder);

            foreach (string path in filePaths)
            {
                if (path.Contains(_nameOfFileToDownload))
                {
                    File.Delete(path);
                    return true;
                }
            }

            return false;
        }
    }
}