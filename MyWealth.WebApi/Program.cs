using Microsoft.EntityFrameworkCore;
using MyWealth.Business.Operations.Comment;
using MyWealth.Business.Operations.Stock;
using MyWealth.Data.Context;
using MyWealth.Data.Repositories;
using MyWealth.Data.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>)); // generic olduðu için typeof kullandýk

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IStockService, StockManager>();

builder.Services.AddScoped<ICommentService, CommentManager>();

builder.Services.AddDbContext<MyWealthDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
