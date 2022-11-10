using Business.Business;
using NUnit.Framework;
using Tests.Utils;

namespace Tests.Tests
{
    public class InsightsPageTests : TestsSetup
    {
        [TestCase(3)]
        public void Validate_TitleOfArticle_MatchesWith_TitleInCarousel(int index)
        {
            try
            {
                LoggerHolder.Logger.Debug($"Run test {nameof(Validate_TitleOfArticle_MatchesWith_TitleInCarousel)} with next parameter: " +
                    $"{nameof(index)}: {index}");

                var home = new HomeContext();

                LoggerHolder.Logger.Info("Open About page");
                var insights = home.OpenInsightsPage();

                LoggerHolder.Logger.Info("Go to element in Carousel");
                insights.GoToElementInCarousel(index);

                LoggerHolder.Logger.Info("Note name of article");
                var nameOfArticle = insights.GetNameOfCarouselArticle();

                LoggerHolder.Logger.Info("Click on Learn More link");
                var carousel = insights.ClickOnLearnMoreLink();

                LoggerHolder.Logger.Info("Check name of article");
                Assert.True(carousel.GetNameOfCarouselArticle().Contains(nameOfArticle, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                LoggerHolder.Logger.Fatal(ex);
                ScreenshotMaker.TakeScreenshot();
                throw;
            }
        }
    }
}
