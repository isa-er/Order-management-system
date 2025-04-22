using FluentValidation;
using FluentValidation.AspNetCore;
using SignalR.BusinessLayer.Abstract;
using SignalR.BusinessLayer.Concrete;
using SignalR.BusinessLayer.ValidationRules.BookingValidations;
using SignalR.DataAccessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;
using SignalR.DataAccessLayer.EntityFramework;
using SignalRapi.Hubs;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


// CORS POLICY
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", builder =>  // CorsPolicy adýnda bir politika oluþturuyoruz.
    {
        builder.AllowAnyHeader()  // gelen herhangi bir baþlýða izin ver
        .AllowAnyMethod() // herhangi bir metoda izin ver
        .WithOrigins("http://localhost:4200") // herhangi bir kaynaktan gelen isteðe izin ver
        .SetIsOriginAllowed((host) => true) // dýþarýdan gelen herhangir bir saðlayýcýya izin ver
        .AllowCredentials(); // kimlik doðrulama izni ver
    });
});

builder.Services.AddSignalR();// SignalR'ý projeye dahil ediyoruz.


// Veritabaný baðlantýsý
builder.Services.AddDbContext<SignalRContext>();


// AutoMapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());// Automapper'ý projeye dahil ediyoruz.



builder.Services.AddControllersWithViews()
    .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler =
    ReferenceHandler.IgnoreCycles);


// birbirinden kalýtým almýþ sýnýflarý çaðýrýyoruz. REGISTERAZTION
builder.Services.AddScoped<IAboutService, AboutManager>();// Scope olarak ekliyoruz IAboutService görünce AboutManager sýnýfýný çaðýr.
builder.Services.AddScoped<IAboutDal, EfAboutDal>();


builder.Services.AddScoped<IBookingService, BookingManager>();
builder.Services.AddScoped<IBookingDal, EfBookingDal>();


builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<ICategoryDal, EfCategoryDal>();

builder.Services.AddScoped<IContactService, ContactManager>();
builder.Services.AddScoped<IContactDal, EfContactDal>();


builder.Services.AddScoped<IDiscountService, DiscountManager>();
builder.Services.AddScoped<IDiscountDal, EfDiscountDal>();


builder.Services.AddScoped<IFeatureService, FeatureManager>();
builder.Services.AddScoped<IFeatureDal, EfFeatureDal>();


builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddScoped<IProductDal, EfProductDal>();

builder.Services.AddScoped<ISocialMediaService, SocialMediaManager>();
builder.Services.AddScoped<ISocialMediaDal, EfSocialMediaDal>();

builder.Services.AddScoped<ITestimonialService, TestimonialManager>();
builder.Services.AddScoped<ITestimonialDal, EfTestimonialDal>();

builder.Services.AddScoped<IOrderDetailService, OrderDetailManager>();
builder.Services.AddScoped<IOrderDetailDal, EfOrderDetailDal>();

builder.Services.AddScoped<IOrderService, OrderManager>();
builder.Services.AddScoped<IOrderDal, EfOrderDal>();

builder.Services.AddScoped<IMoneyCaseService, MoneyCaseManager>();
builder.Services.AddScoped<IMoneyCaseDal, EfMoneyCaseDal>();

builder.Services.AddScoped<IMenuTableService, MenuTableManager>();
builder.Services.AddScoped<IMenuTableDal, EfMenuTableDal>();

builder.Services.AddScoped<ISliderService, SliderManager>();
builder.Services.AddScoped<ISliderDal, EfSliderDal>();

builder.Services.AddScoped<IBasketService, BasketManager>();
builder.Services.AddScoped<IBasketDal, EfBasketDal>();

builder.Services.AddScoped<INotificationService, NotificationManager>();
builder.Services.AddScoped<INotificationDal, EfNotificationDal>();

builder.Services.AddScoped<IMessageService, MessageManager>();
builder.Services.AddScoped<IMessageDal, EfMessageDal>();



builder.Services.AddFluentValidationAutoValidation(config =>
{
    config.DisableDataAnnotationsValidation = true; // FluentValidation'ý kullanabilmek için bu satýrý ekliyoruz.
});

builder.Services.AddValidatorsFromAssemblyContaining<CreateBookingValidation>(); // FluentValidation'ý projeye dahil ediyoruz. FluentValidation'ý kullanabilmek için bu satýrý ekliyoruz. createBookingValidation için kullandýk burada







builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseCors("CorsPolicy"); // Yukarýda tanýmlamýþ olduðumuz CorsPolicy'yi kullanýma açýyoruz.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<SignalRHub>("/signalrhub");// SignalR'ý projeye dahil ediyoruz.
// localhost://1234/swagger/category/index  >>> index'e istek atýyoruz burda
// localhost://1234/signalrhub   >>>> burada ise SÝGNALRHUB'A ÝSTEK ATMAMIZI SAÐLAYACAK

app.Run();
