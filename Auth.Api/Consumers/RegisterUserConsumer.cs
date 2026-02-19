using Intercore.shared.DTOs.Auth;

namespace Auth.Api.Consumers;

using MassTransit;

public class RegisterUserConsumer : IConsumer<RegisterMessages.RegisterRequest>
{
    
    private readonly ILogger<RegisterUserConsumer> _logger;
    
    public RegisterUserConsumer(ILogger<RegisterUserConsumer> logger)
    {
        _logger = logger;
    }
    public async Task Consume(ConsumeContext<RegisterMessages.RegisterRequest> context)
    {
        var datos = context.Message;

        _logger.LogInformation("============================================");
        _logger.LogInformation($"[KAFKA AUTH] Recibido usuario: {datos.Email}");
      
        _logger.LogInformation("============================================");

       
        await Task.CompletedTask;
    }
}