using ALLAH.Application.interfaces.Contexts;
using ALLAH.Common.Dto;
using Microsoft.EntityFrameworkCore;

namespace ALLAH.Application.Services.Products.queries.GetCategories
{
    public class GetCategoriesService : IGetCategoriesService
    {
        private readonly IDataBaseContext dataBaseContext;
        public GetCategoriesService(IDataBaseContext context)
        {
            dataBaseContext = context;
        }

        public long? ParentId { get; private set; }

        public ResultDto<List<CategoreisDto>> Execute(long? UserId)
        {
            var categories = dataBaseContext.Categories
                 .Include(p => p.ParentCategory)
                 .Include(p => p.SubCategories)
                 .Where(p => p.ParentCategoryId == ParentId)
                 .ToList().Select(p => new CategoreisDto
                 {
                     Id = p.Id,
                     Name = p.Name,
                     Parent = p.ParentCategory != null ? new 
                     ParentCategoryDto
                     {
                         Id = p.ParentCategory.Id,
                         Name = p.ParentCategory.Name,
                     }
                     : null,
                     HasChild = p.SubCategories.Count() > 0 ? true : false,

                 }).ToList();
            return new ResultDto<List<CategoreisDto>>
            {
                Data = categories,
                IsSucsess = true,
                Message = "لیست باموقیت برگشت داده شد"

            };
        }
        public class CategoreisDto
        {
            public long Id { get; set; }
            public string Name { get; set; }

            public bool HasChild { get; set; }
            public ParentCategoryDto Parent { get; set; }
        }

        public class ParentCategoryDto
        {
            public long Id { get; set; }
            public string Name { get; set; }

        }

    }

}
