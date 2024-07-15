using ALLAH.Application.Services.Orders.Queries.GetUserOrdersss;
using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Site.Controllers
{
    [Authorize]
    //فقط کاربرانی که لاگبن کردند میتوانند درخواست بدهند
    public class OrdersController
        : Controller
    {
        private readonly IGetUserOrderService getUserOrderService;
        public OrdersController(IGetUserOrderService getUserOrder)
        {
            getUserOrderService = getUserOrder;
        }
        public IActionResult Index()
        {
            long userId =ClaimUtility.GetUserId(User).Value;
            return View(getUserOrderService.Execute(userId).Data);
        }
    }
}
