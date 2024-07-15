using ALLAH.Application.interfaces.Contexts;
using ALLAH.Application.interfaces.FacadPattern;
using ALLAH.Application.Services.Products.Commands.AddNewCategory;
using ALLAH.Application.Services.Products.Commands.AddNewProduct;
using ALLAH.Application.Services.Products.queries.GetAllCategories;
using ALLAH.Application.Services.Products.queries.GetCategories;
using ALLAH.Application.Services.Products.queries.GetProductDetailForAdmin;
using ALLAH.Application.Services.Products.queries.GetProductDetailForSite;
using ALLAH.Application.Services.Products.queries.GetProductForAdmin;
using ALLAH.Application.Services.Products.queries.GetProductForSite;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALLAH.Application.Services.Products.FacadPattern
{
    public class ProductFacad : IProductFacad
    {
        private readonly IDataBaseContext dataBaseContext;
        private readonly IHostingEnvironment _environment;
        public ProductFacad(IDataBaseContext context, IHostingEnvironment hostingEnvironment)
        {
            dataBaseContext = context;
            _environment = hostingEnvironment;
        }

        private AddNewCategoryService _addNewCategory;
        public AddNewCategoryService AddNewCategoryService
        {
            get { return _addNewCategory = _addNewCategory ?? new AddNewCategoryService(dataBaseContext); }
        }

        private IGetCategoriesService _getCategoriesService;
        public IGetCategoriesService GetCategoriesService
        {
            get
            {
                return _getCategoriesService ?? new GetCategoriesService(dataBaseContext);
            }
        }

        private AddNewProductService _addnewproductservice;

        public AddNewProductService AddNewProductService
        {
            get
            {
                return _addnewproductservice = _addnewproductservice ?? new AddNewProductService(dataBaseContext, _environment);
            }
        }
        private IGetAllCategoriesService _getAllCategoriesService;
        public IGetAllCategoriesService GetAllCategoriesService
        {
            get
            {
                return _getAllCategoriesService = _getAllCategoriesService ?? new GetAllCategoriesService(dataBaseContext);
            }
        }

        private IGetProductForAdminService _getproductforadminservice;
        public IGetProductForAdminService GetProductForAdminService
        {
            get { return _getproductforadminservice = _getproductforadminservice ?? new GetProductForAdminService(dataBaseContext); }
        }

        private IGetProductDetailForAdminService _getproductdetailforadminservice;
        public IGetProductDetailForAdminService GetProductDetailForAdminService
        {
            get { return _getproductdetailforadminservice = _getproductdetailforadminservice ?? new GetProductDetailForAdminService(dataBaseContext); }
        }
        private IGetProductForSiteService _getproductforsiteservice;
        public IGetProductForSiteService GetProductForSiteService
        {
            get { return _getproductforsiteservice = _getproductforsiteservice ?? new GetProductForSiteService(dataBaseContext); }
        }

        private IGetProductDetailForSiteService _getProductDetailForSiteService;
        public IGetProductDetailForSiteService GetProductDetailForSiteService
        {
            get
            {
                return _getProductDetailForSiteService = _getProductDetailForSiteService ?? new GetProductDetailForSiteService(dataBaseContext);
            }
        }
    }
}




