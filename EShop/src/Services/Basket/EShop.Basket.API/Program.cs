using EShop.Basket.API.Consumers;
using EShop.Basket.API.Services;
using MassTransit;
using MassTransit.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(brConfig =>
{
    brConfig.AddConsumer<BasketProductPriceDiscountConsumer>();
    brConfig.UsingRabbitMq((context, config) =>
    {
        config.Host("rabbitmq", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

       config.ReceiveEndpoint("basket-service", e =>
       {
           e.ConfigureConsumer<BasketProductPriceDiscountConsumer>(context);
       });

    });

});

builder.Services.AddGrpc();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGrpcService<CustomBasketService>();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
