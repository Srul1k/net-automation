using Business.ApplicationInterfaces;
using Core.Core;
using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace Business.Business
{
    public class SearchContext
    {
        private SearchPage page = new SearchPage();

        public void ShowAllResults()
        {
            DriverHolder.Driver.ShowAllResults(page.ViewMoreLink);
        }

        public ReadOnlyCollection<IWebElement> GetArticles()
        {
            return page.GetArticles();
        }
    }
}
