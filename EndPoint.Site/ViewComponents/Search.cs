using ALLAH.Application.Services.Common.Queries.GetCategory;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Site.ViewComponents
{
    public class Search:ViewComponent
    {
        private readonly IGetCategoryService getCategoryService;
        public Search(IGetCategoryService getCategory)
        {
            getCategoryService = getCategory;
        }

        public IViewComponentResult Invoke()
        {
            return View(viewName: "Search" , getCategoryService.Execute().Data);
        }
    }
}
