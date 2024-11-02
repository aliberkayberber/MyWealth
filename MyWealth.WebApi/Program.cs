using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyWealth.Business.DataProtection;
using MyWealth.Business.Operations.Comment;
using MyWealth.Business.Operations.Portfolio;
using MyWealth.Business.Operations.Setting;
using MyWealth.Business.Operations.Stock;
using MyWealth.Business.Operations.User;
using MyWealth.Data.Context;
using MyWealth.Data.Entities;
using MyWealth.Data.Repositories;
using MyWealth.Data.UnitOfWork;
using MyWealth.WebApi.Middlewares;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Settings to authorize via swagger
builder.Services.AddSwaggerGen(options =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "Jwt",
        Name = "Jwt Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Put **_ONLY_** your Jwt Bearer Token on Texbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme,
        }

    };

    options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {jwtSecurityScheme, Array.Empty<string>() },
    });
});

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>)); // We used typeof because it is generic.

//We determine the service life time and introduce that we will produce the manager produced from the interface.
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(); 
builder.Services.AddScoped<IStockService, StockManager>();
builder.Services.AddScoped<ICommentService, CommentManager>();
builder.Services.AddScoped<IUserService, UserManager>();
builder.Services.AddScoped<IDataProtection, DataProtection>();
builder.Services.AddScoped<IPortfolioService, PortfolioManager>();
builder.Services.AddScoped<ISettingService , SettingManager>();
builder.Services.AddTransient<ExceptionMiddleware>();

// We introduce the database
builder.Services.AddDbContext<MyWealthDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

//for the method we use the user's password.
var keysDirectory = new DirectoryInfo(Path.Combine(builder.Environment.ContentRootPath, "App_Data", "Keys"));
builder.Services.AddDataProtection()
       .SetApplicationName("BookingApp")
       .PersistKeysToFileSystem(keysDirectory);


// jwt options
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {

        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {

            ValidateIssuer = true, // Issuer validation� yap.
            ValidIssuer = builder.Configuration["Jwt:Issuer"], // appsettingsteki de�er.
            ValidateAudience = true, // Audience validation� yap.
            ValidAudience = builder.Configuration["Jwt:Audience"], // appsettingsteki de�er
            ValidateLifetime = true, // Ge�erlilik zaman� validation� yap.
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!)) //  appsettingsteki key.
        };
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMaintenanceMode(); // maintenance mode 
app.ConfigureExceptionHandlingMiddleware(); // global exception handler

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
