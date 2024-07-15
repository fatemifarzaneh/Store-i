using ALLAH.Application.interfaces.FacadPattern;
using ALLAH.Application.Services.Common.Queries.GetHomePageImages;
using ALLAH.Application.Services.Common.Queries.GetSlider;
using ALLAH.Application.Services.Products.queries.GetProductForSite;
using EndPoint.Site.Models;
using EndPoint.Site.Models.ViewModel.HomePages;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EndPoint.Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGetSliderService _getsliderService;
        private readonly IGetHomePageImagesService _getHomePageImagesService;
        private readonly IProductFacad _productFacad;

        public HomeController(ILogger<HomeController> logger, IGetSliderService getSliderService, IGetHomePageImagesService getHomePageImagesService, IProductFacad productFacad)
        {
            _logger = logger;
            _getsliderService = getSliderService;
            _getHomePageImagesService = getHomePageImagesService;
            _productFacad = productFacad;
        }

        
        public IActionResult Index()
        {


            HomePageViewModel homePage = new HomePageViewModel()
            {
                Sliders = _getsliderService.Execute().Data,
               PageImages=_getHomePageImagesService.Execute().Data,
                Camera=_productFacad.GetProductForSiteService.Execute(Ordering.theNewest,null,1,6,1).Data.products,


           };

           return View(homePage);          
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
