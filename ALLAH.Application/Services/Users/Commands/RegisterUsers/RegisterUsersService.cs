using ALLAH.Application.interfaces.Contexts;
using ALLAH.Common;
using ALLAH.Common.Dto;
using ALLAH.Domain.Entities.Users;
using System.Text.RegularExpressions;

namespace ALLAH.Application.Services.Users.Commands.RegisterUsers
{
    public  class RegisterUsersService : IRegisterUsersService
    {
        private readonly IDataBaseContext _dataBaseContext;

        public RegisterUsersService(IDataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;

        }
        public ResultDto<ResultRegisterUsersDto> Execute(RequestRegisterUsersDto request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Email))
                {
                    return new ResultDto<ResultRegisterUsersDto>()
                    {
                        Data = new ResultRegisterUsersDto
                        {
                            UserId = 0
                        },
                        IsSucsess = false,
                        Message = "پست الکترونیک را وارد نمایید"


                    };
                }
                if (string.IsNullOrWhiteSpace(request.FullName))
                {
                    return new ResultDto<ResultRegisterUsersDto>()
                    {
                        Data = new ResultRegisterUsersDto
                        { UserId = 0 },
                        IsSucsess = false,
                        Message = "نام را وارد کنید",
                    };
                }

                if (string.IsNullOrWhiteSpace(request.Password))
                {
                    return new ResultDto<ResultRegisterUsersDto>()
                    {
                        Data = new ResultRegisterUsersDto { UserId = 0 },
                        IsSucsess = false,
                        Message = "پسورد را وارد کنید"
                    };
                }
                if (request.Password != request.RePassword)
                {
                    return new ResultDto<ResultRegisterUsersDto>()
                    {
                        Data = new ResultRegisterUsersDto { UserId = 0 },
                        IsSucsess = false,
                        Message = "رمز عبور با تکرار آن برابر نیست"
                    };
                }
                string emailRegex = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[A-Z0-9.-]+\.[A-Z]{2,}$";

                var match = Regex.Match(request.Email, emailRegex, RegexOptions.IgnoreCase);
                if (!match.Success)
                {
                    return new ResultDto<ResultRegisterUsersDto>()
                    {
                        Data = new ResultRegisterUsersDto()
                        {
                            UserId = 0,
                        },
                        IsSucsess = false,
                        Message = "ایمیل خودرا به درستی وارد نمایید"
                    };
                }
                var passwordHasher = new PasswordHasher();
                var hashedPassword = passwordHasher.HashPassword(request.Password);

                User user = new User
                {
                    Email = request.Email,
                    FullName = request.FullName,
                    Password = hashedPassword,
                    IsActive = true

                };

                List<UserInRole> userInRoles = new List<UserInRole>();

                foreach (var item in request.roles)
                {
                    var roles = _dataBaseContext.Roles.Find(item.Id);
                    userInRoles.Add(new UserInRole
                    {
                        Role = roles,
                        RoleId = roles.Id,
                        User = user,
                        UserId = user.Id,


                    });
                }

                user.UserInRoles = userInRoles;

                _dataBaseContext.Users.Add(user);

                _dataBaseContext.SaveChanges();

                return new ResultDto<ResultRegisterUsersDto>()
                {
                    Data = new ResultRegisterUsersDto()
                    {
                        UserId = user.Id,
                    },
                    IsSucsess = true,
                    Message = "ثبت نام کاربر با موفقیت انجام شد"


                };
            }
            catch (Exception)
            {
                return new ResultDto<ResultRegisterUsersDto>()
                {
                    Data = new ResultRegisterUsersDto()
                    { UserId = 0 },
                    IsSucsess = false,
                    Message = "ثبت نام انجام نشد"
                };
            }
        }

        }
        public class RequestRegisterUsersDto
        {
            public string FullName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string RePassword { get; set; }
            public List<RolesRegisterUserDto> roles { get; set; }
        }
        public class ResultRegisterUsersDto
        {
            public long UserId { get; set; }
        }
        public class RolesRegisterUserDto
        {
            public long Id { get; set; }
        }
    }

