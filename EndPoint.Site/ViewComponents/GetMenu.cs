using ALLAH.Application.Services.Common.Queries.GetMenuItem;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Site.ViewComponents
{
    public class GetMenu:ViewComponent
    {
        private readonly IGetMenuItemService _menuItem;
        public GetMenu(IGetMenuItemService getMenuItem)
        {
            _menuItem = getMenuItem;
        }

        public IViewComponentResult Invoke()
        {
            var menuItem = _menuItem.Execute();
            return View(viewName: "GetMenu", menuItem.Data);
        }
    }
}
