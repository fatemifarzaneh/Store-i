using ALLAH.Application.Services.Users.Commands.EditUser;
using ALLAH.Application.Services.Users.Commands.RegisterUsers;
using ALLAH.Application.Services.Users.Commands.RemoveUser;
using ALLAH.Application.Services.Users.Commands.UserStatusChange;
using ALLAH.Application.Services.Users.queries.GetRoles;
using ALLAH.Application.Services.Users.queries.GetUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static ALLAH.Application.Services.Users.Commands.RegisterUsers.RegisterUsersService;
using static ALLAH.Application.Services.Users.queries.GetUsers.GetUsersService;


namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly IGetUsersService _getUsersService;
        private readonly IGetRolesService _getRolesService;
        private readonly IRegisterUsersService _registerUsersService;
        private readonly IRemoveUserService _removeUserService;
        private readonly IUserStatusChangeService _userStatusChangeService;
        private readonly IEditUserService _editUserService;
        public UsersController(IGetUsersService getUsersService , IGetRolesService getRolesService,IRegisterUsersService registerUsersService,IRemoveUserService removeUserService, IUserStatusChangeService userStatusChangeService, IEditUserService editUserService  )
        {
            _getUsersService = getUsersService;
            _getRolesService = getRolesService;
            _registerUsersService = registerUsersService;
            _removeUserService = removeUserService;
            _userStatusChangeService = userStatusChangeService;
            _editUserService = editUserService;
        }

       
        public IActionResult Index(string searchkey, int page = 1)
        {
            return View(_getUsersService.Execute(new RequestGetusersDto
            {               
                SearchKey = searchkey,
                Page = page,
            }));
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Roles = new SelectList(_getRolesService.Execute().Data, "Id", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult Create(string Email,string FullName,long RoleId, string Password ,string RePassword)
        {
            var result = _registerUsersService.Execute(new RequestRegisterUsersDto
            {
                Email = Email,
                FullName = FullName,
                roles = new List<RolesRegisterUserDto>()
                {
                    new RolesRegisterUserDto
                    {
                        Id = RoleId

                    }
                   
                },
                Password = Password,
                RePassword = RePassword


            });
            return Json(result);
        }
        public IActionResult Delete(long userId) 
        {
            return Json(_removeUserService.Execute(userId));
        }
        [HttpPost]
        public IActionResult UserStatusChange(long userId)
        {
            return Json(_userStatusChangeService.Execute(userId));
        }

        public IActionResult Edit(long userId , string FullName)
        {

            return Json(_editUserService.Execute(new RequestEdituserDto
            {
                Fullname = FullName,
                userId = userId,
            }));
        }
    }
}
