using System.Text;
using Application.Modules.FileServe;
using Application.Modules.FileServe.Models;
using Application.Modules.Movies.Interfaces;
using Application.Modules.Movies.Services;
using Application.Modules.Tickets.Interfaces;
using Application.Modules.Tickets.Services;
using Auth.Api.Consumers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Auth.Api.Extensions;
using Auth.Api.shared;
using Domain.Interfaces;
using Infrastructure.Adapters;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Intercore.shared.DTOs.Auth;
using Intercore.shared.Constans.KAFKA.topics;
using Intercore.shared.DTOs;
using Intercore.shared.middlewares;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);




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




builder.Services.AddScoped<ITicketRepository, TicketRepository>();

builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IMovieService, MovieService>();




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

app.MapHealthChecks("/health");

    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }


    app.UseMiddleware<KafkaLoggingMiddleware>();

    app.MapControllers();
    app.Run();



