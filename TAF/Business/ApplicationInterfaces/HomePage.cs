using OpenQA.Selenium;

namespace Business.ApplicationInterfaces
{
    class HomePage
    {
        public By CareersLink = By.XPath("//a[@class='top-navigation__item-link' and text()='Careers']");

        public By AboutLink = By.XPath("//a[@class='top-navigation__item-link' and text()='About']");

        public By InsightsLink = By.XPath("//a[@class='top-navigation__item-link' and text()='Insights']");

        public By AcceptCookiesButton = By.Id("onetrust-accept-btn-handler");

        public By MagnifierButton = By.CssSelector("button.header-search__button");

        public By SearchInput = By.Id("new_form_search");

        public By FindButton = By.XPath("//button[@class='header-search__submit']");
    }
}
