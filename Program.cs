using BurgerBuilder.WebApi.ViewModels.Order;
using BurgerBuilder.WebApi.ViewModels.User;
using BurgerBuilderApi.Database;
using BurgerBuilderApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BBContext>(options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IOrderService, OrderService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseDeveloperExceptionPage();

#region Users
app.MapPost("users/login", (LoginViewModel loginViewModel, [FromServices] IUserService userService) =>
{
    return userService.Login(loginViewModel);
}).WithName("Login");

app.MapPost("users/signup", (SingUpViewModel singUpViewModel, [FromServices] IUserService userService) =>
{
    return userService.SignUp(singUpViewModel);
}).WithName("signup");

app.MapPost("users/recoverPassword", (RecoverEmailViewModel recoverEmailViewModel, [FromServices] IUserService userService) =>
{
    return userService.RecoverPassword(recoverEmailViewModel);
}).WithName("RecoverPassword");

app.MapPost("users/isTokenValid", (TokenViewModel tokenViewModel, [FromServices] IUserService userService) =>
{
    return userService.IsTokenValid(tokenViewModel);
}).WithName("IsTokenValid");

app.MapGet("users/confirmmail", ([FromServices] IUserService userService) =>
{
    return false;//userService.ConfirmMail(tokenViewModel);
}).WithName("Confirmmail");
#endregion

#region Orders
app.MapPost("orders/AddOrder", (AddOrderViewModel addOrderViewModel, [FromServices] IOrderService orderService) =>
{
    return orderService.AddOrder(addOrderViewModel);
}).WithName("AddOrder");

app.MapPost("orders/GetPagedAllOrders", (GetAllOrdersViewModel getAllOrdersViewModel, [FromServices] IOrderService orderService) =>
{
    return orderService.GetAllOrders(getAllOrdersViewModel, 0);
}).WithName("GetPagedAllOrders");

app.MapPost("orders/GetAllOrders", ([FromServices] IOrderService orderService) =>
{
    return orderService.GetAllOrders();
}).WithName("GetAllOrders");

app.MapPost("orders/GetOrder", (GetOrderViewModel getOrderViewModel, [FromServices] IOrderService orderService) =>
{
    return orderService.GetOrder(getOrderViewModel);
}).WithName("GetOrder");

app.MapPost("orders/SaveComment", (SaveCommentViewModel saveCommentViewModel, [FromServices] IOrderService orderService) =>
{
    return orderService.SaveComment(saveCommentViewModel);
}).WithName("SaveComment");
#endregion
#region SafeOrders
app.MapPost("safeOrders/AddOrder", (AddOrderViewModel addOrderViewModel, [FromServices] IOrderService orderService, [FromServices] IUserService userService,[FromHeaderAttribute(Name = "Authorization")] string authorization) =>
{
    try
    {
        string token = authorization.Split(" ")[1];
        if (!userService.IsTokenValid(new TokenViewModel { token = token }).status)
        {
            throw new Exception();
        }
    }
    catch
    {
        throw new Exception("authentication failed");
    }
    return orderService.AddOrder(addOrderViewModel);
}).WithName("SafeAddOrder");

app.MapPost("safeOrders/GetPagedAllOrders", (GetAllOrdersViewModel getAllOrdersViewModel, [FromServices] IOrderService orderService, [FromServices] IUserService userService, [FromHeaderAttribute(Name = "Authorization")] string authorization) =>
{
    try
    {
        string token = authorization.Split(" ")[1];
        if (!userService.IsTokenValid(new TokenViewModel { token = token }).status)
        {
            throw new Exception();
        }
    }
    catch
    {
        throw new Exception("authentication failed");
    }
    return orderService.GetAllOrders(getAllOrdersViewModel, 0);
}).WithName("SafeGetPagedAllOrders");

app.MapPost("safeOrders/GetAllOrders", ([FromServices] IOrderService orderService, [FromServices] IUserService userService, [FromHeaderAttribute(Name = "Authorization")] string authorization) =>
{
    try
    {
        string token = authorization.Split(" ")[1];
        if (!userService.IsTokenValid(new TokenViewModel { token = token }).status)
        {
            throw new Exception();
        }
    }
    catch
    {
        throw new Exception("authentication failed");
    }
    return orderService.GetAllOrders();
}).WithName("SafeGetAllOrders");

app.MapPost("safeOrders/GetOrder", (GetOrderViewModel getOrderViewModel, [FromServices] IOrderService orderService, [FromServices] IUserService userService, [FromHeaderAttribute(Name = "Authorization")] string authorization) =>
{
    try
    {
        string token = authorization.Split(" ")[1];
        if (!userService.IsTokenValid(new TokenViewModel { token = token }).status)
        {
            throw new Exception();
        }
    }
    catch
    {
        throw new Exception("authentication failed");
    }
    return orderService.GetOrder(getOrderViewModel);
}).WithName("SafeGetOrder");

app.MapPost("safeOrders/SaveComment", (SaveCommentViewModel saveCommentViewModel, [FromServices] IOrderService orderService, [FromServices] IUserService userService, [FromHeaderAttribute(Name = "Authorization")] string authorization) =>
{
    try
    {
        string token = authorization.Split(" ")[1];
        if (!userService.IsTokenValid(new TokenViewModel { token = token }).status)
        {
            throw new Exception();
        }
    }
    catch
    {
        throw new Exception("authentication failed");
    }
    return orderService.SaveComment(saveCommentViewModel);
}).WithName("SafeSaveComment");
#endregion


app.Run();