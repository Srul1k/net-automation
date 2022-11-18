using Business.Business;
using Core.Core;
using NUnit.Framework;
using Tests.Utils;

namespace Tests.Tests
{
    [Parallelizable(scope: ParallelScope.All)]
    public class CareersPageTests : TestsSetup
    {
        [TestCase(".NET", "All Locations")]
        public void Validate_SearchForPosition_BasedOnCriteria_WorksAsExpected(string language, string location)
        {
            try
            {
                LoggerHolder.Logger.Debug($"Run test {nameof(Validate_SearchForPosition_BasedOnCriteria_WorksAsExpected)} " +
                    $"with next parameters: {nameof(language)}: {language} | {nameof(location)}: {location}");

                var home = new HomeContext();

                LoggerHolder.Logger.Info("Open Careers page");
                var careers = home.OpenCareersPage();

                LoggerHolder.Logger.Info("Enter language in field input");
                careers.EnterLanguageInFieldInput(language);

                LoggerHolder.Logger.Info("Select location");
                careers.SelectLocation(location);

                LoggerHolder.Logger.Info("Select remote option");
                careers.SelectRemoteOption();

                LoggerHolder.Logger.Info("Click on Find button");
                careers.ClickOnFindButton();

                LoggerHolder.Logger.Info("Show all results");
                careers.ShowAllResults();

                LoggerHolder.Logger.Info("Select last position");
                careers.ClickOnViewAndApplyButtonForLatestPosition();

                LoggerHolder.Logger.Info("Check result");
                Assert.IsTrue(DriverHolder.Driver.PageSource.Contains(language));
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
