using Microsoft.EntityFrameworkCore;
using Store.Application.Models;
using Store.Application.Products.Dtos;
using Store.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var cs = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<DataContext>(options => { options.UseSqlServer(cs); });

builder.Services.AddMemoryCache();
builder.Services.AddAutoMapper(typeof(Program), typeof(BaseModel));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(GetProductByIdResponse)));


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
