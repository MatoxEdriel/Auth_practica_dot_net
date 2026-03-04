using Application.Modules.Tickets.Interfaces;
using Application.Modules.Tickets.Services;
using Auth.Api.Consumers;
using Auth.Api.Extensions;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Intercore.shared.DTOs.Auth;
using Intercore.shared.Constans.KAFKA.topics;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);


var kafkaHost = builder.Configuration["KafkaConfig:Host"] ?? "localhost:9092";
var consumerGroup = builder.Configuration["KafkaConfig:ConsumerGroup"] ?? "auth-service-group";


builder.WebHost.ConfigureKestrel((context, options) =>
{
    var kestrelSection = context.Configuration.GetSection("Kestrel");

    if (kestrelSection.Exists())
    {
        options.Configure(kestrelSection);
        
    }
});




builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddHealthChecks();

builder.Services.AddSingleton<DapperContext>();


builder.Services.AddScoped<ITicketRepository, TicketRepository>();

builder.Services.AddScoped<ITicketService, TicketService>();




builder.Services.AddMassTransit(x =>
{

    x.UsingInMemory((context, cfg) => cfg.ConfigureEndpoints(context));

    x.AddRider(rider =>
    {
        rider.AddAuthConsumers(consumerGroup, kafkaHost);

    });
});


var app = builder.Build();

app.MapHealthChecks("/health");

    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }
    app.MapControllers();
    app.Run();



