using ALLAH.Application.interfaces.Contexts;
using ALLAH.Common.Dto;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALLAH.Application.Services.Common.Queries.GetSlider
{
    public interface IGetSliderService
    {
        public ResultDto<List<SliderDto>> Execute();
    }

    public class GetSliderService :IGetSliderService
    {
        private readonly IDataBaseContext dataBaseContext;
        public GetSliderService(IDataBaseContext dataBase )
        {
            dataBaseContext = dataBase;
        }

        public ResultDto<List<SliderDto>> Execute()
        {
            var sliders= dataBaseContext.Sliders.OrderByDescending(p=>p.Id).ToList().Select(p=> new SliderDto
            {
                Link=p.link,
                Src=p.Src
            }).ToList();

            return new ResultDto<List<SliderDto>>()
            {
                IsSucsess = true,
                Data = sliders
            };
        }
    }
    public class SliderDto
    {
        public string Src { get; set; }
        public string Link { get; set; }

    }
}
