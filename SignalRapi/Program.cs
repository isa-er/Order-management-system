using FluentValidation;
using FluentValidation.AspNetCore;
using SignalR.BusinessLayer.Abstract;
using SignalR.BusinessLayer.Concrete;
using SignalR.BusinessLayer.Container;
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





builder.Services.AddFluentValidationAutoValidation(config =>
{
    config.DisableDataAnnotationsValidation = true; // FluentValidation'ý kullanabilmek için bu satýrý ekliyoruz.
});

builder.Services.AddValidatorsFromAssemblyContaining<CreateBookingValidation>(); // FluentValidation'ý projeye dahil ediyoruz. FluentValidation'ý kullanabilmek için bu satýrý ekliyoruz. createBookingValidation için kullandýk burada

builder.Services.ContainerDependencies();

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
