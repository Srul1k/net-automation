using OpenQA.Selenium;

namespace Business.ApplicationInterfaces
{
    class InsightsPage
    {
        public By LearnMoreLink = By.CssSelector("a.button-ui[tabindex='0']");

        public By NameOfArticle = By.CssSelector("div.owl-item.active.center h2");

        public By ElementInCarousel(int index) =>
            By.CssSelector($".slider__navigation > .slider__dot:nth-child({index})");

        public By ActiveElementInCarousel(int index) =>
            By.CssSelector($".slider__navigation > .slider__dot:nth-child({index})[tabindex='0']");
    }
}
