using ALLAH.Application.interfaces.FacadPattern;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize("Operator")]
    public class CategoriesController : Controller
    {
        private readonly IProductFacad _productFacad;
        public CategoriesController(IProductFacad productFacad)
        {
            _productFacad = productFacad;
            
        }
        public IActionResult Index(long ParentId)
        {
            return View(_productFacad.GetCategoriesService.Execute(ParentId).Data);
        }
        [HttpGet]
        public IActionResult AddNewCategory(long? ParentId)
        {
            ViewBag.ParentId = ParentId;    
            return View();
        }

        [HttpPost]
        public IActionResult AddNewCategory(long? ParentId,string Name)
        {
            var result =_productFacad.AddNewCategoryService.Execute(ParentId, Name);
            return Json(result);
        }
    }
}
