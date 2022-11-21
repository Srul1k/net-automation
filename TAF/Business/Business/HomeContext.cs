using Business.ApplicationInterfaces;
using Core.Core;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Business.Business
{
    public class HomeContext
    {
        private HomePage page = new HomePage();

        public void AcceptAllCookies()
        {
            new WebDriverWait(DriverHolder.Driver, TimeSpan.FromSeconds(10))
                .Until(ExpectedConditions.ElementToBeClickable(page.AcceptCookiesButton));

            var element = DriverHolder.Driver.FindElement(page.AcceptCookiesButton);
            Actions actions = new Actions(DriverHolder.Driver);
            actions.MoveToElement(element);
            actions.Perform();
            element.Click();
        }

        public CareersContext OpenCareersPage()
        {
            DriverHolder.Driver.FindElement(page.CareersLink).Click();
            return new CareersContext();
        }

        public AboutContext OpenAboutPage()
        {
            DriverHolder.Driver.FindElement(page.AboutLink).Click();
            return new AboutContext();
        }

        public InsightsContext OpenInsightsPage()
        {
            DriverHolder.Driver.FindElement(page.InsightsLink).Click();
            return new InsightsContext();
        }

        public void ClickOnMagnifierButton()
        {
            DriverHolder.Driver.FindElement(page.MagnifierButton).Click();
        }

        public void EnterSearchQueryInFieldInput(string searchQuery)
        {
            var searchInput = DriverHolder.Driver.FindElement(page.SearchInput);
            searchInput.SendKeys(searchQuery);
        }

        public SearchContext ClickOnFindButton()
        {
            DriverHolder.Driver.FindElement(page.FindButton).Click();
            return new SearchContext();
        }
    }
}
