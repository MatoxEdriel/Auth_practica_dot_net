using Auth.Api.Consumers;
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

builder.Services.AddMassTransit(x =>
{

    x.UsingInMemory((context, cfg) => cfg.ConfigureEndpoints(context));

    x.AddRider(rider =>
    {
        rider.AddConsumer<RegisterUserConsumer>();

        rider.UsingKafka((context, k) =>
        {

            k.Host(kafkaHost);
            k.TopicEndpoint<Intercore.shared.DTOs.Auth.RegisterMessages.RegisterRequest>(
                AuthTopics.RegisterUserCommand,
                consumerGroup,
                e => { e.ConfigureConsumer<RegisterUserConsumer>(context); });
        });

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



