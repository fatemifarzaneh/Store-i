using ALLAH.Application.interfaces.Contexts;
using ALLAH.Common.Dto;
using ALLAH.Domain.Entities.Finances;
using ALLAH.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALLAH.Application.Services.Finances.Commands.AddRequestPay
{
    public interface IAddRequestPayService
    {
        ResultDto<ResultRequestPayDto> Execute(int Amount, long userId);
    }
    public class AddRequestPayService : IAddRequestPayService

    {
        private readonly IDataBaseContext dataBaseContext;
        public AddRequestPayService(IDataBaseContext context)
        {
            dataBaseContext = context;
        }
        
        public ResultDto<ResultRequestPayDto> Execute(int Amount, long UserId)
        {
            var user = dataBaseContext.Users.Find(UserId);
            RequestPay requestPay = new RequestPay()
            {
                Amount = Amount,
                Guid = Guid.NewGuid(),
                IsPay = false,

            };
            dataBaseContext.requestPays.Add(requestPay);
            dataBaseContext.SaveChanges();
            return new ResultDto<ResultRequestPayDto>()
            {
                Data = new ResultRequestPayDto
                {
                    Guid = requestPay.Guid,
                    Amount = requestPay.Amount,
                    Email=user.Email,
                    RequestPayId=requestPay.Id,

                },
                IsSucsess = true,
            };

        }
    }
       
    
    public class ResultRequestPayDto
    {
        public Guid Guid { get; set; }
        public int Amount { get; set; }
        public string Email {  get; set; }
        public long RequestPayId {  get; set; }
        

    }
}
