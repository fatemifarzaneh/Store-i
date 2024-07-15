using ALLAH.Application.Services.Products.Commands.AddNewCategory;
using ALLAH.Application.Services.Products.Commands.AddNewProduct;
using ALLAH.Application.Services.Products.queries.GetAllCategories;
using ALLAH.Application.Services.Products.queries.GetCategories;
using ALLAH.Application.Services.Products.queries.GetProductDetailForAdmin;
using ALLAH.Application.Services.Products.queries.GetProductDetailForSite;
using ALLAH.Application.Services.Products.queries.GetProductForAdmin;
using ALLAH.Application.Services.Products.queries.GetProductForSite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALLAH.Application.interfaces.FacadPattern
{
    public interface IProductFacad
    {
        AddNewCategoryService AddNewCategoryService { get; }
        IGetCategoriesService GetCategoriesService { get; }

        AddNewProductService AddNewProductService { get; }
        IGetAllCategoriesService GetAllCategoriesService { get; }

        IGetProductForAdminService GetProductForAdminService { get; }

        IGetProductDetailForAdminService GetProductDetailForAdminService { get; }

        IGetProductForSiteService GetProductForSiteService {  get; }
        IGetProductDetailForSiteService GetProductDetailForSiteService { get; }
    }
}
