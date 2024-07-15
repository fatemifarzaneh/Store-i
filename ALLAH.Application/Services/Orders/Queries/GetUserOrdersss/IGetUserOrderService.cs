using ALLAH.Application.interfaces.Contexts;
using ALLAH.Common.Dto;
using ALLAH.Domain.Entities.Orders;
using ALLAH.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALLAH.Application.Services.Orders.Queries.GetUserOrdersss
{
    public interface IGetUserOrderService
    {
        ResultDto<List<GetUserOrderDto>> Execute(long UserId);
            
    }
    public class GetUserOrderService : IGetUserOrderService
    {
        private readonly IDataBaseContext _context;

        public GetUserOrderService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<GetUserOrderDto>> Execute(long UserId)
        {

            var orders = _context.Orders
                    .Include(p => p.OrderDetails)
                    .ThenInclude(p => p.Product)
                    .Where(p => p.UserId == UserId)
                    .OrderByDescending(p => p.Id).ToList().Select(p => new GetUserOrderDto
                    {
                        OrderId = p.Id,
                        OrderState = p.OrderState,
                        RequestPayId = p.RequestPayId,
                        OrdersDetails = p.OrderDetails.Select(o => new OrdersDetailsDto
                        {

                            Count = o.Count,
                            OrderDetailId = o.Id,
                            Price = o.Price,
                            ProductId = o.ProductId,
                            ProductName = o.Product.Name,
                        }).ToList(),
                    }).ToList();

            return new ResultDto<List<GetUserOrderDto>>()
            {
                Data = orders,
                IsSucsess = true,
            };



        }
    }



    public class GetUserOrderDto
    {
        public long OrderId { get; set; }
        public OrderState OrderState { get; set; }
        public long RequestPayId { get; set; }
        public List<OrdersDetailsDto> OrdersDetails { get; set; }
    }
    public class OrdersDetailsDto
    {


        public long OrderDetailId { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }

        public int Price { get; set; }
        public int Count { get; set; }
    }


}
