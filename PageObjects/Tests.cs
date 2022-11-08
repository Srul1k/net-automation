using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PageObjects.Extensions;
using PageObjects.PageObjects;

namespace PageObjects
{
    public class Tests
    {
        private ChromeDriver _driver;
        private static string _nameOfFileToDownload = "EPAM_Systems_Company_Overview.pdf";
        private static string _downloadFolder = Environment.GetEnvironmentVariable("USERPROFILE") + "\\Downloads";
        private static string _filePath = Path.Combine(_downloadFolder, _nameOfFileToDownload);

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

            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
        }

        [TestCase(".NET", "All Locations")]
        public void Validate_SearchForPosition_BasedOnCriteria_WorksAsExpected(string language, string location)
        {
            var homePage = new HomePage(_driver);
            var careersPage = new CareersPage(_driver);

            homePage.CareersLink.Click();
            careersPage.KeywordInput.SendKeys(language);
            careersPage.LocationUnwrapper.Click();

            careersPage.SelectLocation(location);

            careersPage.RemoteOption.Click();
            careersPage.FindButton.Click();
            careersPage.ShowAllResults();

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

            searchPage.ShowAllResults();

            Assert.IsTrue(searchPage.GetArticles().Any(a => a.Text.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)));
        }

        [Test]
        public void Validate_FileDownloadFunction_WorksAsExpected()
        {
            var homePage = new HomePage(_driver);
            var aboutPage = new AboutPage(_driver);

            homePage.AboutLink.Click();
            _driver.MoveToElement(aboutPage.DownloadLink);
            aboutPage.DownloadLink.Click();

            AssertIsFileDownloaded();
        }

        [TestCase(3)]
        public void Validate_TitleOfArticle_MatchesWith_TitleInCarousel(int index)
        {
            var homePage = new HomePage(_driver);
            var insigthsPage = new InsightsPage(_driver);
            var carouselPage = new CarouselPage(_driver);

            homePage.InsightsLink.Click();

            insigthsPage.GoToElementInCarousel(index);
            var nameOfArticle = insigthsPage.GetNameOfCarouselArticle();
            insigthsPage.LearnMoreLink.Click();

            Assert.True(carouselPage.NameOfArticle.Text.Contains(nameOfArticle, StringComparison.OrdinalIgnoreCase));
        }

        private void AssertIsFileDownloaded()
        {
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(2000);

                if (File.Exists(_filePath))
                {
                    Assert.Pass();
                }
            }

            Assert.Fail();
        }
    }
}