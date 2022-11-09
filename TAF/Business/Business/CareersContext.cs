using Business.ApplicationInterfaces;
using Core.Core;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Business.Business
{
    public class CareersContext
    {
        private CareersPage page = new CareersPage();

        public void EnterLanguageInFieldInput(string language)
        {
            var keywordInput = DriverHolder.Driver.FindElement(page.KeywordInput);
            keywordInput.SendKeys(language);
        }

        public void SelectLocation(string location)
        {
            DriverHolder.Driver.FindElement(page.LocationUnwrapper).Click();

            if (location.Contains("All", StringComparison.Ordinal))
            {
                DriverHolder.Driver
                    .FindElement(page.AllLocationsItem(location))
                    .Click();
            }
            else
            {
                DriverHolder.Driver
                    .FindElement(page.LocationItem(location))
                    .FindElement(page.ParentLocationItem)
                    .Click();

                DriverHolder.Driver
                    .FindElement(page.LocationItem(location))
                    .Click();
            }
        }

        public void SelectRemoteOption()
        {
            new WebDriverWait(DriverHolder.Driver, TimeSpan.FromSeconds(5))
                .Until(ExpectedConditions.ElementToBeClickable(page.RemoteOption));

            DriverHolder.Driver.FindElement(page.RemoteOption).Click();
        }

        public void ClickOnFindButton()
        {
            DriverHolder.Driver.FindElement(page.FindButton).Click();
        }

        public void ShowAllResults()
        {
            DriverHolder.Driver.ShowAllResults(page.ViewMoreLink);
        }

        public void ClickOnViewAndApplyButtonForLatestPosition()
        {
            var latestPositionViewAndApplyButton = DriverHolder.Driver.FindElement(page.LatestPositionViewAndApplyButton);

            DriverHolder.Driver.MoveToElement(latestPositionViewAndApplyButton);
            latestPositionViewAndApplyButton.Click();
        }
    }
}
