using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyWealth.Business.DataProtection;
using MyWealth.Business.Operations.Comment;
using MyWealth.Business.Operations.Portfolio;
using MyWealth.Business.Operations.Stock;
using MyWealth.Business.Operations.User;
using MyWealth.Data.Context;
using MyWealth.Data.Entities;
using MyWealth.Data.Repositories;
using MyWealth.Data.UnitOfWork;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>)); // generic oldu�u i�in typeof kulland�k

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IStockService, StockManager>();

builder.Services.AddScoped<ICommentService, CommentManager>();

builder.Services.AddScoped<IUserService, UserManager>();
builder.Services.AddScoped<IDataProtection, DataProtection>();
builder.Services.AddScoped<IPortfolioService, PortfolioManager>();

builder.Services.AddDbContext<MyWealthDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
var keysDirectory = new DirectoryInfo(Path.Combine(builder.Environment.ContentRootPath, "App_Data", "Keys"));
builder.Services.AddDataProtection()
       .SetApplicationName("BookingApp")
       .PersistKeysToFileSystem(keysDirectory);


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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
