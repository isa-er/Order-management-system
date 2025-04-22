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
    opt.AddPolicy("CorsPolicy", builder =>  // CorsPolicy ad�nda bir politika olu�turuyoruz.
    {
        builder.AllowAnyHeader()  // gelen herhangi bir ba�l��a izin ver
        .AllowAnyMethod() // herhangi bir metoda izin ver
        .WithOrigins("http://localhost:4200") // herhangi bir kaynaktan gelen iste�e izin ver
        .SetIsOriginAllowed((host) => true) // d��ar�dan gelen herhangir bir sa�lay�c�ya izin ver
        .AllowCredentials(); // kimlik do�rulama izni ver
    });
});

builder.Services.AddSignalR();// SignalR'� projeye dahil ediyoruz.


// Veritaban� ba�lant�s�
builder.Services.AddDbContext<SignalRContext>();


// AutoMapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());// Automapper'� projeye dahil ediyoruz.



builder.Services.AddControllersWithViews()
    .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler =
    ReferenceHandler.IgnoreCycles);


// birbirinden kal�t�m alm�� s�n�flar� �a��r�yoruz. REGISTERAZTION
builder.Services.AddScoped<IAboutService, AboutManager>();// Scope olarak ekliyoruz IAboutService g�r�nce AboutManager s�n�f�n� �a��r.
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
    config.DisableDataAnnotationsValidation = true; // FluentValidation'� kullanabilmek i�in bu sat�r� ekliyoruz.
});

builder.Services.AddValidatorsFromAssemblyContaining<CreateBookingValidation>(); // FluentValidation'� projeye dahil ediyoruz. FluentValidation'� kullanabilmek i�in bu sat�r� ekliyoruz. createBookingValidation i�in kulland�k burada







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



app.UseCors("CorsPolicy"); // Yukar�da tan�mlam�� oldu�umuz CorsPolicy'yi kullan�ma a��yoruz.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<SignalRHub>("/signalrhub");// SignalR'� projeye dahil ediyoruz.
// localhost://1234/swagger/category/index  >>> index'e istek at�yoruz burda
// localhost://1234/signalrhub   >>>> burada ise S�GNALRHUB'A �STEK ATMAMIZI SA�LAYACAK

app.Run();
