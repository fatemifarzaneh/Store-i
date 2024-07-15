using ALLAH.Application.Services.Common.Queries.GetHomePageImages;
using ALLAH.Application.Services.Common.Queries.GetSlider;
using ALLAH.Application.Services.Products.queries.GetProductForSite;
using ALLAH.Common.Dto;

namespace EndPoint.Site.Models.ViewModel.HomePages
{
    public class HomePageViewModel
    {
       public List<SliderDto> Sliders { get; set; }
        public List<HomePageImagesDto> PageImages { get; set; }
        public List<ProductForSiteDto> Camera { get; set; }
        public List<ProductForSiteDto> Mobile { get; set; }
        public List<ProductForSiteDto> Laptop { get; set; }

    }
}
