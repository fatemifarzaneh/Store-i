using ALLAH.Application.interfaces.Contexts;
using ALLAH.Common.Dto;
using ALLAH.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALLAH.Application.Services.Orders.Commands.AddNewOrderService
{
    public interface IAddNewOrderService
    {
        ResultDto Execute(RequestAddNewOrderServiceDto request);

    }

    public class AddNewOrderService : IAddNewOrderService
    {
        private readonly IDataBaseContext dataBaseContext;
        public AddNewOrderService(IDataBaseContext Context)
        {
            dataBaseContext= Context;
        }
      
        public ResultDto Execute(RequestAddNewOrderServiceDto request)
        {

            var user= dataBaseContext.Users.Find(request.UserId);
            var requestpay= dataBaseContext.requestPays.Find(request.RequestPayId);
            var cart = dataBaseContext.Carts.Include(p => p.CartItems).ThenInclude(p=>p.Product).Where(p => p.Id == request.CartId).FirstOrDefault
                ();

            requestpay.IsPay = true;
            requestpay.PayDate = DateTime.Now;
            requestpay.RefId = request.RefId;
            requestpay.Authority= request.Authority;
          
            cart.Finished = true;

            Order order = new Order()
            {
                OrderState = OrderState.Processing,
                Address = "",
                RequestPay = requestpay,
                User = user,


            };
           dataBaseContext.Orders.Add(order);

            List<OrderDetail> orderDetails = new List<OrderDetail>();
            foreach (var item in cart.CartItems)
            {
                OrderDetail orderDetail = new OrderDetail()
                {
                    Count = item.Count,
                    Price = item.Product.Price,
                    Order = order,


                };
                orderDetails.Add(orderDetail);
                
            }
            dataBaseContext.OrderDetails.AddRange(orderDetails);

            dataBaseContext.SaveChanges();

            return new ResultDto()
            {
                IsSuccess = true,
                Message = ""
            };
        }
    }
    public class RequestAddNewOrderServiceDto
    {
        public long CartId { get; set; }
        public long RequestPayId { get; set; }
        public long UserId { get; set; }

        public string Authority {  get; set; }
        public long RefId { get; set; }
    }
}