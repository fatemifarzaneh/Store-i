using ALLAH.Application.interfaces.Contexts;
using ALLAH.Common;
using ALLAH.Common.Dto;
using Microsoft.EntityFrameworkCore;

namespace ALLAH.Application.Services.Products.queries.GetProductForSite
{
    public class GetProductForSiteService : IGetProductForSiteService
    {
        private readonly IDataBaseContext dataBaseContext;
        public GetProductForSiteService(IDataBaseContext dataBase)
        {
            dataBaseContext = dataBase;
        }
        public ResultDto<ResultProductForSiteDto> Execute(Ordering ordering, string SearchKey, int Page, int PageSize, long? CatId)
        {
            int TotalaRow = 0;
             var productQuery = dataBaseContext.Products.Include(p => p.ProductImagess).AsQueryable();

            if (CatId != null)
            {
                productQuery = productQuery.Where(p => p.CategoryId == CatId || p.Category.ParentCategoryId == CatId).AsQueryable();
            }
            if (!string.IsNullOrWhiteSpace(SearchKey))
            {
                productQuery = productQuery.Where(p => p.Name.Contains(SearchKey) || p.Brand.Contains(SearchKey)).AsQueryable();
            }
            switch (ordering)
            {
                case Ordering.NotOrder:
                    productQuery = productQuery.OrderByDescending(p => p.Id).AsQueryable();
                    break;
                case Ordering.MostVisited:
                    productQuery = productQuery.OrderByDescending(p => p.ViewCount).AsQueryable();
                    break;
                case Ordering.Bestselling:
                    break;
                case Ordering.MostPopular:
                    break;
                case Ordering.theNewest:
                    productQuery = productQuery.OrderByDescending(p => p.Id).AsQueryable();
                    break;
                case Ordering.Cheapest:
                    productQuery = productQuery.OrderBy(p => p.Price).AsQueryable();
                    break;
                case Ordering.theMostExpensive:
                    productQuery = productQuery.OrderByDescending(p => p.Price).AsQueryable();
                    break;
                default:
                    break;
            }
            var product = productQuery.ToPaged(Page, PageSize, out TotalaRow);
            
            Random rd = new Random();
            return new ResultDto<ResultProductForSiteDto>()
            {
                Data = new ResultProductForSiteDto
                {
                    products = product.Select(p => new ProductForSiteDto
                    {
                        Id = p.Id,
                        Star = rd.Next(1, 5),
                        Title = p.Name,
                        ImageSrc = p.ProductImagess.FirstOrDefault().Src,
                        Price=p.Price


                    }).ToList()


                },
                IsSucsess = true,
                Message = ""
            };

        }
    }
}