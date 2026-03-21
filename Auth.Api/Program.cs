using System.Text;
using Application.Modules.FileServe;
using Application.Modules.FileServe.Models;
using Application.Modules.Movies.Services;
using Auth.Api.shared;
using Infrastructure.Adapters;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Intercore.shared.Constans.KAFKA.topics;
using Intercore.shared.DTOs;
using Intercore.shared.middlewares;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };


    });

builder.Services.AddAuthorization();




var transferSettings = new FileTransferSettings();
builder.Configuration.GetSection("FileTransferSettings").Bind(transferSettings);

builder.Services.AddSingleton(transferSettings);

var kafkaHost = builder.Configuration["KafkaConfig:Host"] ?? "localhost:9092";

if (transferSettings.Protocol.ToUpper() == "SFTP")
{
    builder.Services.AddTransient<IFileTransferService, SftpAdapter>();
}
else
{
    builder.Services.AddTransient<IFileTransferService, FtpAdapter>();
}



builder.WebHost.ConfigureKestrel((context, options) =>
{
    var kestrelSection = context.Configuration.GetSection("Kestrel");

    if (kestrelSection.Exists())
    {
        options.Configure(kestrelSection);
        
    }
});


builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalResponseFilter>();
});


builder.Services.AddOpenApi();
builder.Services.AddHealthChecks();

builder.Services.AddSingleton<DapperContext>();





builder.Services.Scan(selector => selector
    .FromAssembliesOf(typeof(MovieService), typeof(MovieRepository))

    .AddClasses(classes => classes.Where(c =>
        c.Name.EndsWith("Service") ||
        c.Name.EndsWith("Repository")
    ))
    .AsMatchingInterface()
    .WithScopedLifetime()
);




builder.Services.AddMassTransit(x =>
{

    x.UsingInMemory((context, cfg) => cfg.ConfigureEndpoints(context));

    x.AddRider(rider =>
    {

        rider.AddProducer<CreateAppLogDto>(KafkaTopics.AppLogs);
        rider.AddProducer<CreateAccessLogDto>(KafkaTopics.AccessLogs);
        rider.AddProducer<CreateExceptionLogDto>(KafkaTopics.ExceptionLogs);
        
        rider.UsingKafka((context, k) =>
        {
            k.Host(kafkaHost);
        });
        
        
    });
});


var app = builder.Build();

app.UseMiddleware<KafkaLoggingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapHealthChecks("/health");

    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }
    
    app.MapControllers();
    app.Run();



