using ALLAH.Application.Services.Carts;
using ALLAH.Application.Services.Finances.Commands.AddRequestPay;
using ALLAH.Application.Services.Finances.Queries.GetRequestPay;
using ALLAH.Application.Services.Orders.Commands.AddNewOrderService;
using ALLAH.Domain.Entities.Finances;
using ALLAH.Persistance.Migrations;
using Dto.Payment;
using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using ZarinPal.Class;

namespace EndPoint.Site.Controllers
{
    [Authorize("Customer")]
    public class PayController : Controller
    {
        private readonly IAddRequestPayService _payService;
        private readonly ICartService _cartService;
        private readonly CookiesManager _cookiesManager;
        private readonly Payment _payment;
        private readonly Authority _authority;
        private readonly Transactions _transactions;
        private readonly IGetRequestPayService getRequestPayService;
        private readonly IAddNewOrderService addNewOrderService;
        public PayController(IAddRequestPayService addRequestPayService,ICartService cartService, IGetRequestPayService getRequestPay , IAddNewOrderService orderService)
        {
            _payService = addRequestPayService;  
            _cartService = cartService;
            _cookiesManager = new CookiesManager();
            var expose = new Expose();
            _payment = expose.CreatePayment();
            _authority = expose.CreateAuthority();
            _transactions = expose.CreateTransactions();
            getRequestPayService = getRequestPay;
            addNewOrderService= orderService;   
        }
        public async Task< IActionResult> Index()
        {
            long? UserId = ClaimUtility.GetUserId(User);
            var cart =_cartService.GetMyCart(_cookiesManager.GetBrowserId(HttpContext),UserId);

            if(cart.Data.SumAmount > 0)
            {
                var requestpay = _payService.Execute(cart.Data.SumAmount, UserId.Value);
                //ارسال درگاه پرداخت
                var result = await _payment.Request(new DtoRequest()
                {
                    Mobile = "09121112222",
                    CallbackUrl = "https://localhost:44339/Pay/Verify?guid={requestPay.Data.guid}",
                    Description = "پرداخت فاکتور شماره:" +requestpay.Data.RequestPayId,
                    Email = requestpay.Data.Email,
                    Amount = requestpay.Data.Amount,
                    MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX"
                }, ZarinPal.Class.Payment.Mode.sandbox);
                return Redirect($"https://sandbox.zarinpal.com/pg/StartPay/{result.Authority}");

            }
            else
            {
                return RedirectToAction("Index", "Cart");
            }
          
            return View();
        }

        public async Task<IActionResult> verify(Guid guid,string authority ,string atatus )
        {
            var requestPay = getRequestPayService.Execute(guid);

            var verification = await _payment.Verification(new DtoVerification
            {
                Amount = requestPay.Data.Amount,
                MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
                Authority = authority
            }, Payment.Mode.sandbox);

            long? UserId = ClaimUtility.GetUserId(User);
            var cart = _cartService.GetMyCart(_cookiesManager.GetBrowserId(HttpContext), UserId);
            if (verification.Status == 100)
            {
                addNewOrderService.Execute(new RequestAddNewOrderServiceDto
                {
                    CartId = cart.Data.CartId,
                    UserId = UserId.Value,
                    RequestPayId = requestPay.Data.Id,


                });
                ///redirect to orders
                return RedirectToAction("Index", "Orders");
            }
            else
            {

            }
            return View();  
        }
    }
}
