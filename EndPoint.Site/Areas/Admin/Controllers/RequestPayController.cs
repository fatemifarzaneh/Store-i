using ALLAH.Application.Services.Finances.Queries.GetRequestPayForAdmin;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RequestPayController : Controller
    {
        private readonly IGetRequestPayForAdminService getRequestPayForAdminService;
        public RequestPayController(IGetRequestPayForAdminService requestPayForAdminService)
        {
            getRequestPayForAdminService = requestPayForAdminService;
            
        }
        public IActionResult Index()
        {
            return View(getRequestPayForAdminService.Execute().Data);
        }
    }
}
