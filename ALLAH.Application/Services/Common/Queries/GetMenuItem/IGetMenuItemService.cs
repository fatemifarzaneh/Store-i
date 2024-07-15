using ALLAH.Application.interfaces.Contexts;
using ALLAH.Common.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALLAH.Application.Services.Common.Queries.GetMenuItem
{
    public interface IGetMenuItemService
    {
        ResultDto<List<MenuItemDto>> Execute();
    }
    public class GetMenuItemService : IGetMenuItemService
    {
        private readonly IDataBaseContext dataBaseContext;
        public GetMenuItemService(IDataBaseContext context)
        {
            dataBaseContext = context;
        }

        public ResultDto<List<MenuItemDto>> Execute()
        {
            var category = dataBaseContext.Categories.Include(p => p.SubCategories)
                .Where(p=>p.ParentCategoryId==null).ToList()
                  .Select(p => new MenuItemDto
                  {
                      CatId = p.Id,
                      Name = p.Name,
                      Child = p.SubCategories.ToList().Select(child => new MenuItemDto
                      {
                          Name = child.Name,
                          CatId = child.Id,

                      }).ToList(),
                  }).ToList();

            return new ResultDto<List<MenuItemDto>>()
            {
                Data = category,
                IsSucsess = true,

            };
        }
    }

    public class MenuItemDto
    {
        public long CatId { get; set; }
        public string Name { get; set; }
        public List<MenuItemDto> Child { get; set;}
    }
}
