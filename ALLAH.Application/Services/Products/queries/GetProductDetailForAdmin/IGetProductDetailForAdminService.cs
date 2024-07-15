using ALLAH.Application.interfaces.Contexts;
using ALLAH.Application.Services.Products.queries.GetProductForAdmin;
using ALLAH.Common;
using ALLAH.Common.Dto;
using ALLAH.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALLAH.Application.Services.Products.queries.GetProductDetailForAdmin
{
    public interface IGetProductDetailForAdminService
    {
        ResultDto<ProducDetaitForAdminDto> Execute(long Id);
    }
    public class GetProductDetailForAdminService : IGetProductDetailForAdminService
    {
        private readonly IDataBaseContext dataBaseContext;
        public GetProductDetailForAdminService(IDataBaseContext dataBase)
        {
            dataBaseContext = dataBase;
        }
        public ResultDto<ProducDetaitForAdminDto> Execute(long Id)
        {
            var product = dataBaseContext.Products.Include(p => p.Category).ThenInclude(p => p.ParentCategory).
                 Include(p => p.ProductFeaturess).Include(p => p.ProductImagess).Where(p => p.Id == Id).FirstOrDefault();

            if (product == null)
            {
                return new ResultDto<ProducDetaitForAdminDto>
                {
                    IsSucsess = false,
                    Message = "Product not found.",
                };
            }

            return new ResultDto<ProducDetaitForAdminDto>()
            {
                Data = new ProducDetaitForAdminDto()
                {
                    Brand = product.Brand,
                    Category = GetCategory(product.Category),
                    Description = product.Description,
                    Displayed = product.Displayed,
                    Inventory = product.inventory,
                    Price = product.Price,
                    Name = product.Name,
                    Id = product.Id,
                    Features = product.ProductFeaturess.ToList().Select(p => new ProductDetailFeatureDto
                    {
                        Id = p.Id,
                        DisplayName = p.DisplayName,
                        Value = p.Value,
                    }).ToList(),
                    Images = product.ProductImagess.ToList().Select(p => new ProductDetailImagesDto
                    {
                        Id = p.Id,
                        Src = p.Src,

                    }).ToList(),
                },
                IsSucsess = true,
                Message = "",
            };
        }
        private string GetCategory(Category category)
        {
            string result = category.ParentCategory != null ? $"{category.ParentCategory.Name} - " : "";
            return result += category.Name;
        }
    }


    public class ProducDetaitForAdminDto
    {

        public long Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Inventory { get; set; }
        public bool Displayed { get; set; }
        public List<ProductDetailFeatureDto> Features { get; set; }
        public List<ProductDetailImagesDto> Images { get; set; }
    }
    public class ProductDetailFeatureDto
    {
        public long Id { get; set; }
        public string DisplayName { get; set; }
        public string Value { get; set; }
    }
    public class ProductDetailImagesDto
    {

        public long Id { get; set; }
        public string Src { get; set; }

    }
}

