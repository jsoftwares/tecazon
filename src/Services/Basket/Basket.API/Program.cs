using Basket.API.gRPCServices;
using Basket.API.Repositories;
using Common.Logging;
using Discount.gRPC.Protos;
using MassTransit;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//Configure SeriLog for logging to Elasticsearch
builder.Host.UseSerilog(SeriLogger.Configure);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Redis Configuration
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");
});

//General Configuration
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddAutoMapper(typeof(Program));
//builder.Services.AddAutoMapper(typeof(StartupBase)); Core 5

//Grpc configuration
/**We register d DiscountGrpcService we injected in BasketController & also registered the DiscountProtoService
 * bcos if you right click DiscountGrpcService & click "Go to Definition, you will see it uses DiscountProtoServiceClient
 * which is generated from VS for consuming the gRPC service as a client. For gRPC client registration, we need to 
 * know the URL of d Discount gRPC server**/
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>
    (o => o.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]));
builder.Services.AddScoped<DiscountGrpcService>();

//MassTransit-RabbitMQ configuration
builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
    });
});
//builder.Services.AddMassTransitHostedService(); this is registerred by default in MassTransit v8 and does not need to be done explicitely anymore

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
