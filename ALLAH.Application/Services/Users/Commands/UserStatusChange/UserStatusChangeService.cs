using ALLAH.Application.interfaces.Contexts;
using ALLAH.Common.Dto;

namespace ALLAH.Application.Services.Users.Commands.UserStatusChange
{
    public class UserStatusChangeService : IUserStatusChangeService
    {
        private readonly IDataBaseContext dataBaseContext;
        public UserStatusChangeService(IDataBaseContext context)
        {
            dataBaseContext = context;
           
        }
        public ResultDto Execute(long userId)
        {
            var user = dataBaseContext.Users.Find(userId);
            if(user == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "کاربر یافت نشد"
                };
            }
            user.IsActive = !user.IsActive;
            dataBaseContext.SaveChanges();
            string userstate = user.IsActive == true ? "فعال " : "غیرفعال";
            return new ResultDto()
            {
                IsSuccess = true,
                Message = $"کاربر با موفقیت {userstate} شد"
            };
        }
    }

}
