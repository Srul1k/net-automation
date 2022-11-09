using OpenQA.Selenium;

namespace Business.ApplicationInterfaces
{
    class CareersPage
    {
        public By KeywordInput = By.XPath("//input[@placeholder='Keyword']");

        public By LocationUnwrapper = By.CssSelector("b[role]");

        public By RemoteOption = By.XPath("//input[@name='remote']/parent::p");

        public By FindButton = By.XPath("//button[@type='submit' and text()='Find']");

        public By ViewMoreLink = By.ClassName("search-result__view-more");

        public By LatestPositionViewAndApplyButton = By.XPath("(//a[@class='search-result__item-apply'])[last()]");

        public By ParentLocationItem = By.XPath("../../child::strong");

        public By AllLocationsItem(string location) => By.XPath($"//li[@title='{location}']");

        public By LocationItem(string location) => By.XPath($"//li[text()='{location}']");
    }
}
