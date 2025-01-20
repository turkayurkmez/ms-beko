using EShop.Catalog.Application.Contracts;
using EShop.Catalog.Application.Features.Products.Queries;
using EShop.Catalog.Infrastructure.EventHandlers;
using EShop.Catalog.Infrastructure.Persistence;
using EShop.Catalog.Infrastructure.Repositories;
using EShop.Catalog.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using MassTransit;
using EShop.Catalog.Domain.Events;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();





builder.Services.AddInfrastucture(builder.Configuration);
builder.Services.AddApplication();
//tokalaşma deseni nedeniyle, MassTransit konfigürasyonu burada yapılmalı:
builder.Services.AddMassTransit(brConfig =>
{

    //yayıncı ya RabbitMQ'ya mesaj gönderemezse? O zaman outbox pattern kullanılmalı

    brConfig.AddEntityFrameworkOutbox<CatalogDbContext>(efConfig => {

        efConfig.UseSqlServer();
        efConfig.UseBusOutbox();
        efConfig.QueryDelay = TimeSpan.FromSeconds(120);

    });

    

    brConfig.UsingRabbitMq((context, config) =>
    {
        config.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        config.Publish<ProductPriceDiscountedEvent>(x =>
        {
            x.ExchangeType = ExchangeType.Fanout;
            
        });

    });

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
