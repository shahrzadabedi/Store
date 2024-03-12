using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Store.Application.Models;
using Store.Application.Products.Dtos;
using Store.Domain;
using Store.Domain.Contracts;
using Store.Infrastructure;
using Store.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

var cs = builder.Configuration.GetConnectionString("Default");

builder.Services.AddDbContext<DataContext>(options => { options.UseSqlServer(cs); });

builder.Services.Configure<CacheSettings>(builder.Configuration.GetSection(nameof(CacheSettings)));
builder.Services.AddSingleton(service => service.GetRequiredService<IOptions<CacheSettings>>().Value);
builder.Services.AddScoped<IDatabaseInitializer, DatabaseInitializer>();
builder.Services.AddMemoryCache();
builder.Services.AddAutoMapper(typeof(Program), typeof(BaseModel));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(GetProductByIdResponse)));


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>()
        .InitializeDatabaseAsync().Wait();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
