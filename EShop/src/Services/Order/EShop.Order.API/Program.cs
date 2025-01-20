using EShop.EventBus;
using EShop.Order.API.Consumers;
using EShop.Order.API.Observers;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(brConfig =>
{

    /*
     * 2. Problem:
     * 
     * Alıcı (consumer) gelen mesajı alamadığı durumda iki desen yardımınıza koşar:
     *   a. Retry pattern
     *   b. Circuit Breaker pattern
     *   
     *   
     */
    brConfig.AddConsumer<OrderProductPriceDiscountConsumer>(c =>
    {
        c.UseMessageRetry(retry =>
        {
            retry.Intervals(
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10),
                    TimeSpan.FromSeconds(15)
                );

            //retry.Exponential(5, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(60), TimeSpan.FromSeconds(1));
            retry.Handle<TimeoutException>();
            //retry.ConnectRetryObserver(new RetryLoggerObserver(LoggerFactory.Create));

        });

        c.UseCircuitBreaker(cb =>
        {
            cb.TrackingPeriod = TimeSpan.FromMinutes(1); // 1 dakika içinde
            cb.TripThreshold = 15; // 15 hata olursa
            cb.ActiveThreshold = 10; // 10 aktif istek olursa
            cb.ResetInterval = TimeSpan.FromMinutes(5);// 5 dakika sonra resetle (devre kesici ne kadar süre sonra kapansın)
        });
    });
    brConfig.AddConsumer<StockNotAvailableEventConsumer>();
    brConfig.AddConsumer<PaymentSuccessEventConsumer>();
    brConfig.AddConsumer<PaymentFailedEventConsumer>();


    brConfig.UsingRabbitMq((context, config) =>
    {
        config.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        config.ReceiveEndpoint("order-service", e =>
        {
            e.ConfigureConsumer<OrderProductPriceDiscountConsumer>(context);
        });

        config.ConfigureEndpoints(context);

    });

});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/orderCreate", (IPublishEndpoint publishEndPpint, OrderCreateRequest request) =>
{
    var orderItems = request.OrderItems.Select(x => new OrderItem(x.ProductId, x.Quantity, x.Price)).ToList();
    var orderId = new Random().Next(1000, 10000);
    var command = new OrderCreatedCommand(orderId, request.CustomerId, request.CreditCardInfo, orderItems);

    var @event = new OrderCreatedEvent(command);
    
    publishEndPpint.Publish(@event);


});



app.Run();


public record OrderCreateRequest(string CustomerId, string CreditCardInfo, List<OrderItemInRequest> OrderItems);
public record OrderItemInRequest(string ProductId, int Quantity, decimal Price);