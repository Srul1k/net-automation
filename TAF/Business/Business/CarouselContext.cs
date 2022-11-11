using Business.ApplicationInterfaces;
using Core.Core;

namespace Business.Business
{
    public class CarouselContext
    {
        private CarouselPage page = new CarouselPage();

        public string GetNameOfCarouselArticle()
        {
            return DriverHolder.Driver.FindElement(page.NameOfArticle).Text;
        }
    }
}
