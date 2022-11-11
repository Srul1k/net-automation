using Business.Business;
using NUnit.Framework;
using Tests.Utils;

namespace Tests.Tests
{
    public class SearchPageTests : TestsSetup
    {
        [TestCase("BLOCKCHAIN")]
        [TestCase("Cloud")]
        [TestCase("Automation")]
        public void Validate_GlobalSearch_WorksAsExpected(string searchQuery)
        {
            try
            {
                LoggerHolder.Logger.Debug($"Run test {nameof(Validate_GlobalSearch_WorksAsExpected)} with next parameter: " +
                        $"{nameof(searchQuery)}: {searchQuery} ");

                var home = new HomeContext();

                LoggerHolder.Logger.Info("Click on Magnifier button");
                home.ClickOnMagnifierButton();

                LoggerHolder.Logger.Info("Enter search query In field input");
                home.EnterSearchQueryInFieldInput(searchQuery);

                LoggerHolder.Logger.Info("Click on Find button");
                var search = home.ClickOnFindButton();

                LoggerHolder.Logger.Info("Show all results");
                search.ShowAllResults();

                LoggerHolder.Logger.Info("Check articles");
                Assert.IsTrue(search.GetArticles().Any(a => a.Text.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)));
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
