using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ALLAH.Application.Services.Users.queries.GetUsers.GetUsersService;

namespace ALLAH.Application.Services.Users.queries.GetUsers
{
    public interface IGetUsersService
    {
        ResultGetusersDto Execute(RequestGetusersDto request);
    }

}
