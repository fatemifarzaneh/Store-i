using ALLAH.Application.interfaces.FacadPattern;
using ALLAH.Application.Services.Products.Commands.AddNewProduct;
using ALLAH.Application.Services.Products.FacadPattern;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly IProductFacad productFacad;
        public ProductsController(IProductFacad facad)
        {
            productFacad = facad;
        }
        public IActionResult Index(int Page = 1 ,int PageSize = 20)
        {
            return View(productFacad.GetProductForAdminService.Execute(Page, PageSize).Data );
        }

        public IActionResult Detail(long Id)
        {
            return View(productFacad.GetProductDetailForAdminService.Execute(Id).Data);             
        }
        [HttpGet]
        public IActionResult AddNewProduct()
        {
            ViewBag.categories = new SelectList(productFacad.GetAllCategoriesService.Execute().Data, "Id", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult AddNewProduct(RequestAddNewProductDto request, List<AddNewProduct_Features> Features)
        {
            List<IFormFile> images = new List<IFormFile>();
            for (int i = 0; i < Request.Form.Files.Count; i++)
            {
                var file = Request.Form.Files[i];
                images.Add(file);
            }
            request.Images = images;
            request.Features = Features;
                return Json(productFacad.AddNewProductService.Execute(request));
        }
    }
}

