using ALLAH.Application.Services.Carts;
using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Site.ViewComponents
{
    public class Cart : ViewComponent
    {
        private readonly ICartService _cartService;
        private readonly CookiesManager _cookiesManager;
        public Cart(ICartService cartService)
        {
            _cartService = cartService;
            _cookiesManager = new CookiesManager();
        }
       
        public IViewComponentResult Invoke()
        {
            var userId = ClaimUtility.GetUserId(HttpContext.User);
            return View(viewName: "Cart", _cartService.GetMyCart(_cookiesManager.GetBrowserId(HttpContext), userId).Data);
              
        }
    }
    }

