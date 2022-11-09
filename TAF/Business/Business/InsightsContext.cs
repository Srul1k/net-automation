using Business.ApplicationInterfaces;
using Core.Core;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Business.Business
{
    public class InsightsContext
    {
        private InsightsPage page = new InsightsPage();

        public void GoToElementInCarousel(int index)
        {
            DriverHolder.Driver.FindElement(page.ElementInCarousel(index)).Click();

            new WebDriverWait(DriverHolder.Driver, TimeSpan.FromSeconds(10))
                .Until(ExpectedConditions.ElementIsVisible(page.ActiveElementInCarousel(index)));
        }

        public string GetNameOfCarouselArticle()
        {
            return DriverHolder.Driver.FindElement(page.NameOfArticle).Text;
        }

        public CarouselContext ClickOnLearnMoreLink()
        {
            DriverHolder.Driver.FindElement(page.LearnMoreLink).Click();
            return new CarouselContext();
        }
    }
}
