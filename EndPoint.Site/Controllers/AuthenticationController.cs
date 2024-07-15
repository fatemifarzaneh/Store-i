using ALLAH.Application.Services.Users.Commands.RegisterUsers;
using ALLAH.Application.Services.Users.Commands.UserLogin;
using ALLAH.Common.Dto;
using Azure.Core;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using static ALLAH.Application.Services.Users.Commands.RegisterUsers.RegisterUsersService;
using System.Security.Claims;
using System.Text.RegularExpressions;
using EndPoint.Site.Models.ViewModel.AuthenticatioViewModel;
using ALLAH.Domain.Entities.Users;

namespace EndPoint.Site.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IRegisterUsersService _registerUsersService;
        private readonly IUserLoginService _userLogin;


        public AuthenticationController(IRegisterUsersService registerUsersService,IUserLoginService userlogin)
        {
            _registerUsersService = registerUsersService;
            _userLogin = userlogin;
            
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(SignUpViewModel signUp)
        { 
            if (string.IsNullOrWhiteSpace(signUp.FullName) ||
               string.IsNullOrWhiteSpace(signUp.Email) ||
               string.IsNullOrWhiteSpace(signUp.Password) ||
               string.IsNullOrWhiteSpace(signUp.RePassword))
            {
                return Json(new ResultDto { IsSuccess = false, Message = "لطفا تمامی موارد رو ارسال نمایید" });
            }

            if (User.Identity.IsAuthenticated == true)
            {
                return Json(new ResultDto { IsSuccess = false, Message = "شما به حساب کاربری خود وارد شده اید! و در حال حاضر نمیتوانید ثبت نام مجدد نمایید" });
            }
            if (signUp.Password != signUp.RePassword)
            {
                return Json(new ResultDto { IsSuccess = false, Message = "رمز عبور و تکرار آن برابر نیست" });
            }
            if (signUp.Password.Length < 8)
            {
                return Json(new ResultDto { IsSuccess = false, Message = "رمز عبور باید حداقل 8 کاراکتر باشد" });
            }

            string emailRegex = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[A-Z0-9.-]+\.[A-Z]{2,}$";

            var match = Regex.Match(signUp.Email, emailRegex, RegexOptions.IgnoreCase);
            if (!match.Success)
            {
                return Json(new ResultDto { IsSuccess = true, Message = "ایمیل خودرا به درستی وارد نمایید" });
            }


            var signeupResult = _registerUsersService.Execute(new RequestRegisterUsersDto
            {
                Email= signUp.Email,
                FullName = signUp.FullName,
                Password = signUp.Password,
                RePassword = signUp.RePassword,
                roles = new List<RolesRegisterUserDto>()
                {
                     new RolesRegisterUserDto { Id = 3},
                }
            });

            if (signeupResult.IsSucsess == true)
            {
                var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,signeupResult.Data.UserId.ToString()),
                new Claim(ClaimTypes.Email, signUp.Email),
                new Claim(ClaimTypes.Name, signUp.FullName),
                new Claim(ClaimTypes.Role, "Customer"),
            };


                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties()
                {
                    IsPersistent = true
                };
                HttpContext.SignInAsync(principal, properties);

            }
            return Json(signeupResult);
        }

        public IActionResult SignIn(string ReturnUrl="/")
        {
            ViewBag.url = ReturnUrl;
            return View();
        }

        [HttpPost]
        public IActionResult SignIn(string Email, string Password, string url = "/")
        {
            var signupResult = _userLogin.Execute(Email, Password);
            if (signupResult.IsSucsess == true)
            {
                var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,signupResult.Data.UserId.ToString()),
                new Claim(ClaimTypes.Email, Email),
                new Claim(ClaimTypes.Name, signupResult.Data.Name),
                
            };
                foreach (var item in signupResult.Data.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, item));
                }
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties()
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.Now.AddDays(5),
                };
                HttpContext.SignInAsync(principal, properties);

            }
            return Json(signupResult);
        }

    }

}

