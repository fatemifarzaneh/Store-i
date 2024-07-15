using ALLAH.Application.interfaces.Contexts;
using ALLAH.Application.interfaces.FacadPattern;
using ALLAH.Application.Services.Carts;
using ALLAH.Application.Services.Common.Queries.GetCategory;
using ALLAH.Application.Services.Common.Queries.GetHomePageImages;
using ALLAH.Application.Services.Common.Queries.GetMenuItem;
using ALLAH.Application.Services.Common.Queries.GetSlider;
using ALLAH.Application.Services.Finances.Commands.AddRequestPay;
using ALLAH.Application.Services.Finances.Queries.GetRequestPay;
using ALLAH.Application.Services.Finances.Queries.GetRequestPayForAdmin;
using ALLAH.Application.Services.HomePages.AddHomePageImages;
using ALLAH.Application.Services.HomePages.AddNewSlider;
using ALLAH.Application.Services.Orders.Commands.AddNewOrderService;
using ALLAH.Application.Services.Orders.Queries.GetOrdersForAdmin;
using ALLAH.Application.Services.Orders.Queries.GetUserOrdersss;
using ALLAH.Application.Services.Products.FacadPattern;
using ALLAH.Application.Services.Users.Commands.EditUser;
using ALLAH.Application.Services.Users.Commands.RegisterUsers;
using ALLAH.Application.Services.Users.Commands.RemoveUser;
using ALLAH.Application.Services.Users.Commands.UserLogin;
using ALLAH.Application.Services.Users.Commands.UserStatusChange;
using ALLAH.Application.Services.Users.queries;
using ALLAH.Application.Services.Users.queries.GetRoles;
using ALLAH.Application.Services.Users.queries.GetUsers;
using ALLAH.Common.Roles;
using ALLAH.Persistance.Context;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(UserRoles.Admin, policy => policy.RequireRole(UserRoles.Admin));
    options.AddPolicy(UserRoles.Customer, policy => policy.RequireRole(UserRoles.Customer));
    options.AddPolicy(UserRoles.Operator, policy => policy.RequireRole(UserRoles.Operator));
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(options =>
{
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.LoginPath = new PathString("/");
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5.0);
});
builder.Services.AddScoped<IDataBaseContext,DataBaseContext>();
builder.Services.AddScoped<IGetUsersService, GetUsersService>();
builder.Services.AddScoped<IGetRolesService, GetRolesService>();
builder.Services.AddScoped<IRegisterUsersService, RegisterUsersService>();  
builder.Services.AddScoped<IRemoveUserService, RemoveUserService>();
builder.Services.AddScoped<IUserStatusChangeService, UserStatusChangeService>();
builder.Services.AddScoped<IEditUserService, EditUserService>();
builder.Services.AddScoped<IUserLoginService ,UserLoginService>();
builder.Services.AddScoped<IGetUserOrderService, GetUserOrderService>();    
builder.Services.AddScoped<IGetOrdersForAdminService, GetOrdersForAdminService>();  
builder.Services.AddScoped<IGetRequestPayForAdminService,GetRequestPayForAdminService>();

builder.Services.AddScoped<DataBaseContext>();


//facad inject
builder.Services.AddScoped<IProductFacad,ProductFacad>();


builder.Services.AddScoped<IGetMenuItemService, GetMenuItemService>();
builder.Services.AddScoped<IGetCategoryService, GetCategoryService>();
builder.Services.AddScoped<IAddNewSliderService, AddNewSliderService>();
builder.Services.AddScoped<IGetSliderService, GetSliderService>();
builder.Services.AddScoped<IAddHomePageImagesService, AddHomePageImagesService>();
builder.Services.AddScoped<IGetHomePageImagesService, GetHomePageImagesService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IAddRequestPayService, AddRequestPayService>();
builder.Services.AddScoped<IGetRequestPayService, GetRequestPayService>();
builder.Services.AddScoped<IAddNewOrderService, AddNewOrderService>();  

builder.Services.AddDbContext<DataBaseContext>(options =>
          options.UseSqlServer("Data Source=.;Initial Catalog = ALLAHDb;Integrated Security=true;TrustServerCertificate=True;"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();


app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
   ); 

app.Run();
