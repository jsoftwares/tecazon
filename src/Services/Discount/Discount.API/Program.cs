using Common.Logging;
using Discount.API.Extensions;
using Discount.API.Repositories;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//Configure SeriLog for logging to Elasticsearch
builder.Host.UseSerilog(SeriLogger.Configure);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDiscountRepository, DiscountRepository > ();

var app = builder.Build();

/**Creates Discount DB and Table in POstgres and seed in any dummy data once DiscountAPI service starts**/
app.MigrateDatabase<Program>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
