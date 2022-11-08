using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;

namespace PageObjects.PageObjects
{
    public class InsightsPage
    {
        private IWebDriver _driver;

        [FindsBy(How = How.CssSelector, Using = "a.button-ui[tabindex='0']")]
        public IWebElement LearnMoreLink { get; set; }

        public InsightsPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
        }

        public void GoToElementInCarousel(int index)
        {
            _driver.FindElement(By.CssSelector($".slider__navigation > .slider__dot:nth-child({index})")).Click();

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(
                By.CssSelector($".slider__navigation > .slider__dot:nth-child({index})[tabindex='0']")));
        }

        public string GetNameOfCarouselArticle()
        {
            return _driver.FindElement(By.CssSelector("div.owl-item.active.center h2")).Text;
        }
    }
}
