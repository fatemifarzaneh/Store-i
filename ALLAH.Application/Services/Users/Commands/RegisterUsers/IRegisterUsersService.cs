using ALLAH.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ALLAH.Application.Services.Users.Commands.RegisterUsers.RegisterUsersService;

namespace ALLAH.Application.Services.Users.Commands.RegisterUsers
{
    public interface IRegisterUsersService
    {
        ResultDto<ResultRegisterUsersDto> Execute(RequestRegisterUsersDto request);
    }
}