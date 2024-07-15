using ALLAH.Application.interfaces.Contexts;
using ALLAH.Common.Dto;

namespace ALLAH.Application.Services.Users.queries.GetRoles
{
    public class GetRolesService : IGetRolesService
    {
        private readonly IDataBaseContext dataBaseContext;
        public GetRolesService(IDataBaseContext dataBase)
        {
            dataBaseContext = dataBase;
        }
        public ResultDto<List<RolesDto>> Execute()
        {
            var roles = dataBaseContext.Roles.ToList().Select(p => new RolesDto
            {
                Id = p.Id,
                Name = p.Name,

            }).ToList();
            return new ResultDto<List<RolesDto>>()
            {
                Data = roles,
                IsSucsess = true,
                Message = "",
            };
            
        }
    }
}
