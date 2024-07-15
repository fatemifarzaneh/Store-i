using ALLAH.Application.interfaces.Contexts;
using ALLAH.Application.Services.Users.queries.GetUsers;
using ALLAH.Common;

namespace ALLAH.Application.Services.Users.queries.GetUsers
{
    public partial class GetUsersService : IGetUsersService
    {
        private readonly IDataBaseContext _context;
        public GetUsersService(IDataBaseContext dataBaseContext)
        {
          _context = dataBaseContext;
        }
        public ResultGetusersDto Execute(RequestGetusersDto request)
        {
           var users= _context.Users.AsQueryable();
            if(!string.IsNullOrEmpty(request.SearchKey) )
            {
                users= users.Where(p=>p.FullName.Contains(request.SearchKey) && p.Email.Contains(request.SearchKey));
            }
            int rowsCount = 0;
            var userslist=  users.ToPaged(request.Page, 20,out rowsCount).Select(p=> new GetUsersDto
            {
                Email = p.Email,
                Id = p.Id,
                FullName = p.FullName,
                IsActive = p.IsActive,

            }).ToList();
            return new ResultGetusersDto
            {
                Users = userslist,
                Rows = rowsCount
            };
        }
    }
    
}
