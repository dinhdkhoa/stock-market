using Microsoft.EntityFrameworkCore;
using stock_market.Data;
using stock_market.Interfaces;
using stock_market.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IStockRepository, StockRepository>();

var databasePath = DbConfig.GetDbPath();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite($"Data Source={databasePath}");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();

app.Run();

