using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Repository.Interfaces;
using Repository.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ITaskRepository, TaskService>(m=>new TaskService("http://localhost:5267"));
builder.Services.AddScoped<ILoginService, LoginService>(m=>new LoginService("http://localhost:5267"));
builder.Services.AddScoped<IUserService, UserService>(m=>new UserService("http://localhost:5267"));
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(
    options =>
    {
        options.Cookie.HttpOnly = true;
        options.Cookie.Name = "SessionCookie";
        options.Cookie.IsEssential = true;
    });
builder.Services.Configure<CookiePolicyOptions>(
    options =>
    {
        options.CheckConsentNeeded = context => true;
        options.MinimumSameSitePolicy = SameSiteMode.None;
    });
builder.Services.AddAuthorization();
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
app.UseSession();
app.UseCookiePolicy();

app.UseAuthentication();

app.UseAuthorization();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();

