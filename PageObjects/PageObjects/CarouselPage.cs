using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace PageObjects.PageObjects
{
    public class CarouselPage
    {
        private IWebDriver _driver;

        [FindsBy(How = How.CssSelector, Using = "h1.title-ui")]
        public IWebElement NameOfArticle { get; set; }

        public CarouselPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
        }
    }
}
