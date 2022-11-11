using Core.Core;
using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace Business.ApplicationInterfaces
{
    class SearchPage
    {
        public By ViewMoreLink = By.ClassName("search-results__view-more");

        public ReadOnlyCollection<IWebElement> GetArticles() =>
            DriverHolder.Driver.FindElements(By.XPath("//article[@class='search-results__item']"));
    }
}