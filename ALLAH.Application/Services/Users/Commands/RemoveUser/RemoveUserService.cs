using ALLAH.Application.interfaces.Contexts;
using ALLAH.Common.Dto;

namespace ALLAH.Application.Services.Users.Commands.RemoveUser
{
    public class RemoveUserService : IRemoveUserService
    {
        private readonly IDataBaseContext dataBaseContext;
        public RemoveUserService(IDataBaseContext dataBase)
        {
            dataBaseContext = dataBase;
        }
        public ResultDto Execute(long userId)
        {
            var user= dataBaseContext.Users.Find(userId);
            if(user == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "کاربر یافت نشد"
                };
            }
            user.RemovedTime = DateTime.Now;
            user.IsRemoved = true;
            dataBaseContext.SaveChanges();

            return new ResultDto()
            {
                IsSuccess = true,
                Message = "کاربر با موفقیت حذف شد"
            };
        }
    }
}
