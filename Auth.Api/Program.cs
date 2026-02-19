using Auth.Api.Consumers;
using Intercore.shared.Constans.KAFKA.topics;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel((context, options) =>
{
    options.Configure(context.Configuration.GetSection("Kestrel"));
});


builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddMassTransit(x =>
{

    x.UsingInMemory((context, cfg) => cfg.ConfigureEndpoints(context));

    x.AddRider(rider =>
    {
        rider.AddConsumer<RegisterUserConsumer>();

        rider.UsingKafka((context, k) =>
        {

            k.Host("localhost:9092");
            k.TopicEndpoint<Intercore.shared.DTOs.Auth.RegisterMessages.RegisterRequest>(
                AuthTopics.RegisterUserCommand,
                "auth-group-id",
                e => { e.ConfigureConsumer<RegisterUserConsumer>(context); });



        });

    });
});


var app = builder.Build();




    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }


    app.MapControllers();



    app.Run();








