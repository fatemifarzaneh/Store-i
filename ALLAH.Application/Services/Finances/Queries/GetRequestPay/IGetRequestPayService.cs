using ALLAH.Application.interfaces.Contexts;
using ALLAH.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALLAH.Application.Services.Finances.Queries.GetRequestPay
{
    public interface IGetRequestPayService
    {
        ResultDto<RequestPayDto> Execute(Guid guid);
    }
    public class GetRequestPayService: IGetRequestPayService
    {
        private readonly IDataBaseContext dataBaseContext;
        public GetRequestPayService(IDataBaseContext dataBase)
        {
            dataBaseContext = dataBase;
        }

        public ResultDto<RequestPayDto> Execute(Guid guid)
        {
           var requestpay= dataBaseContext.requestPays.Where(p=>p.Guid == guid).FirstOrDefault();
            if (requestpay != null)
            {
                return new ResultDto<RequestPayDto>()
                {
                    Data = new RequestPayDto
                    {
                        Amount = requestpay.Amount,
                        Id=requestpay.Id,
                    },

                };
            }
            else
            {
                throw new Exception("requestpay not found");
            }

         }

      }
    public class RequestPayDto
    {
        public int Amount { get; set; }
        public long Id {  get; set; }
    }
}

   
         


