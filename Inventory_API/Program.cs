using Inventory_API.Data;
using Inventory_API.IRepository;
using Inventory_API.Mappings;
using Inventory_API.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Connection String
builder.Services.AddDbContext<InventoryDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("InventoryConnectionString")));

//Repository
builder.Services.AddScoped<IRawMaterialsRepository, RawMaterialsRepository>();
builder.Services.AddScoped<IPurchasedStocksRepository, PurchasedStocksRepository>();
builder.Services.AddScoped<IMovedStocksRepository, MovedStocksRepository>();

//AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
