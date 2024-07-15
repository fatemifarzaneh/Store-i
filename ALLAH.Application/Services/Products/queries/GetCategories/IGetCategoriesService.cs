using ALLAH.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ALLAH.Application.Services.Products.queries.GetCategories.GetCategoriesService;

namespace ALLAH.Application.Services.Products.queries.GetCategories
{
    public interface IGetCategoriesService
    {
        ResultDto<List<CategoreisDto>> Execute(long? UserId);
    }


   
}
