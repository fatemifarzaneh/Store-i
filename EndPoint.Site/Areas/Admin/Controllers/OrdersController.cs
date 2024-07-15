using ALLAH.Application.Services.Orders.Queries.GetOrdersForAdmin;
using ALLAH.Domain.Entities.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("admin")]
    //[Authorize(Roles = "Admin,Operator")]
    public class OrdersController : Controller
    {
        private readonly IGetOrdersForAdminService getOrdersForAdminService;
        public OrdersController(IGetOrdersForAdminService ordersForAdminService)
        {
            getOrdersForAdminService = ordersForAdminService;   
            
        }
        public IActionResult Index(OrderState orderState)
        {
            return View(getOrdersForAdminService.Execute(orderState).Data);
        }
    }
}
