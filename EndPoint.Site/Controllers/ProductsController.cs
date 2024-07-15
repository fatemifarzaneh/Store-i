using ALLAH.Application.interfaces.FacadPattern;
using ALLAH.Application.Services.Products.queries.GetProductForSite;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Site.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductFacad _productFacad;
        public ProductsController(IProductFacad productFacad)
        {
            _productFacad=productFacad;
        }
        public IActionResult Index(Ordering ordering,string SearchKey,long? CatId = null, int page = 1,int PageSize =20)
        {
            return View(_productFacad.GetProductForSiteService.Execute(ordering,SearchKey,page,PageSize,CatId).Data);
        }

        public ActionResult Detail(long Id)
        {
            return View(_productFacad.GetProductDetailForSiteService.Execute(Id).Data);
            
        }
    }
}
