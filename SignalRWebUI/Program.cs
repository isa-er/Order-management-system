using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using SignalR.DataAccessLayer.Concrete;
using SignalR.EntityLayer.Entities;

var builder = WebApplication.CreateBuilder(args);


var requireAuthorizePolicy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();



builder.Services.AddHttpClient(); // HttpClient'ı projeye dahil ediyoruz. Api'ye bağlanmak için kullanacağız.


builder.Services.AddDbContext<SignalRContext>(); // DbContext'i projeye dahil ediyoruz. Veritabanı işlemleri için kullanacağız.
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<SignalRContext>();


// Add services to the container.




builder.Services.AddControllersWithViews(opt =>
{
    opt.Filters.Add(new AuthorizeFilter(requireAuthorizePolicy)); // Tüm controller'lara authorize ekliyoruz.
});



//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(options =>
//    {
//        options.LoginPath = "/Login/Index"; // Giriş yapma sayfası
//    });


builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Login/Index"; // Giriş yapma sayfası
});



var app = builder.Build();


// 404 sayfası için özel bir yönlendirme yapıyoruz.
app.UseStatusCodePages(async x =>
{
    if (x.HttpContext.Response.StatusCode == 404)
    {
        x.HttpContext.Response.Redirect("/Error/NotFound404Page");
    }
});



if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting(); // 💥 Bu önde olacak

app.UseAuthentication(); // 💥 Sonra bu
app.UseAuthorization();  // 💥 Sonra bu

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
