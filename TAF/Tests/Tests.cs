using Business.Business;
using Business.Data;
using Core.Core;
using NUnit.Framework;

namespace PageObjects
{
    public class Tests
    {
        [SetUp]
        public void StartBrowser()
        {
            DriverHolder.InitDriver(BrowserName.Chrome);
            DriverHolder.Driver.Url = Data.ApplicationUrl;
            DriverHolder.Driver.Manage().Window.Maximize();
            DriverHolder.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            var home = new HomeContext();
            home.AcceptAllCookies();
        }

        [TearDown]
        public void CloseBrowser()
        {
            DriverHolder.Cleanup();

            if (File.Exists(Data.FilePath))
            {
                File.Delete(Data.FilePath);
            }
        }

        [TestCase(".NET", "All Locations")]
        public void Validate_SearchForPosition_BasedOnCriteria_WorksAsExpected(string language, string location)
        {
            var home = new HomeContext();
            var careers = home.ClickOnCareersLink();

            careers.EnterLanguageInFieldInput(language);
            careers.SelectLocation(location);
            careers.SelectRemoteOption();
            careers.ClickOnFindButton();
            careers.ShowAllResults();
            careers.ClickOnViewAndApplyButtonForLatestPosition();

            Assert.IsTrue(DriverHolder.Driver.PageSource.Contains(language));
        }

        [TestCase("BLOCKCHAIN")]
        [TestCase("Cloud")]
        [TestCase("Automation")]
        public void Validate_GlobalSearch_WorksAsExpected(string searchQuery)
        {
            var home = new HomeContext();

            home.ClickOnMagnifierButton();
            home.EnterSearchQueryInFieldInput(searchQuery);

            var search = home.ClickOnFindButton();
            search.ShowAllResults();

            Assert.IsTrue(search.GetArticles().Any(a => a.Text.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)));
        }

        [Test]
        public void Validate_FileDownloadFunction_WorksAsExpected()
        {
            var home = new HomeContext();

            var about = home.ClickOnAboutLink();
            about.DownloadFile(Data.FilePath);

            Assert.IsTrue(File.Exists(Data.FilePath));
        }

        [TestCase(3)]
        public void Validate_TitleOfArticle_MatchesWith_TitleInCarousel(int index)
        {
            var home = new HomeContext();

            var insights = home.ClickOnInsightsLink();
            insights.GoToElementInCarousel(index);

            var nameOfArticle = insights.GetNameOfCarouselArticle();
            var carousel = insights.ClickOnLearnMoreLink();

            Assert.True(carousel.GetNameOfCarouselArticle().Contains(nameOfArticle, StringComparison.OrdinalIgnoreCase));
        }
    }
}