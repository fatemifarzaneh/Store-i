using ALLAH.Application.interfaces.Contexts;
using ALLAH.Common;
using ALLAH.Common.Dto;
using Azure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALLAH.Application.Services.Products.queries.GetProductForAdmin
{
    public interface IGetProductForAdminService
    {
        ResultDto<ProductForAdminDto> Execute(int Page = 1, int PageSize = 20);
    }
    public class GetProductForAdminService : IGetProductForAdminService
    {
        private readonly IDataBaseContext dataBaseContext;
        public GetProductForAdminService(IDataBaseContext context)
        {
            dataBaseContext = context;
        }
        public ResultDto<ProductForAdminDto> Execute(int Page = 1, int PageSize = 20)
        {
            int rowCount = 0;
            var products= dataBaseContext.Products.Include(p => p.Category)
                .ToPaged(Page,PageSize,out rowCount)
                .Select(p => new ProductsFormAdminList_Dto()
                {
                    Id=p.Id,
                    Brand=p.Brand,
                    Category = p.Category.Name,
                    Description=p.Description,
                    Inventory=p.inventory,
                    Name=p.Name,
                    Price=p.Price,
                    Displayed=p.Displayed,


                }).ToList();

            return new ResultDto<ProductForAdminDto>()
            {
                Data = new ProductForAdminDto()
                {
                    Products = products,
                    PageSize = PageSize,
                    CurrentPage = Page,
                    RowCount = rowCount,
                },
                IsSucsess = true,
                Message = ""
            };
        }
    }
    public class ProductForAdminDto
    {
        public int RowCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

        public List<ProductsFormAdminList_Dto> Products { get; set; }
    }
    public class ProductsFormAdminList_Dto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Inventory { get; set; }
        public bool Displayed { get; set; }
    }
}
