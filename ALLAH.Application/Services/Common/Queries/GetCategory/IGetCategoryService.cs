using ALLAH.Application.interfaces.Contexts;
using ALLAH.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALLAH.Application.Services.Common.Queries.GetCategory
{
    public interface IGetCategoryService
    {
        ResultDto<List<CategoryDto>> Execute();
    }
    public class GetCategoryService : IGetCategoryService
    {
        private readonly IDataBaseContext dataBaseContext;
        public GetCategoryService(IDataBaseContext dataBase)
        {
            dataBaseContext = dataBase;
        }
        public ResultDto<List<CategoryDto>> Execute()
        {
            var category = dataBaseContext.Categories.Where(p=>p.ParentCategoryId==null).ToList()
                .Select(p=>new CategoryDto
                {
                    CatId=p.Id,
                    CategoryName=p.Name,

                }).ToList();
            return new ResultDto<List<CategoryDto>>()
            {
                Data = category,
                IsSucsess = true,
            };
        }
    }
    public class CategoryDto
    {
        public long CatId { get; set; }
        public string CategoryName { get; set; }

    }
}
