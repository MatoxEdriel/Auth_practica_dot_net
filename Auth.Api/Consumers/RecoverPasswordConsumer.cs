using Intercore.shared.DTOs.Auth;

namespace Auth.Api.Consumers;
using MassTransit;

public class RecoverPasswordConsumer: IConsumer<RecoveryMessages.RecoverPasswordRequest>
{
    private readonly ILogger<RecoverPasswordConsumer> _logger;
    //logica de evento 
    private readonly ITopicProducer<RecoveryMessages.PasswordRecoveryRequestedEvent> _eventProducer;
    
    public RecoverPasswordConsumer(
        ILogger<RecoverPasswordConsumer> logger,
        ITopicProducer<RecoveryMessages.PasswordRecoveryRequestedEvent> eventProducer)
    {
        _logger = logger;
        _eventProducer = eventProducer;
    }
    
    public async Task Consume(ConsumeContext<RecoveryMessages.RecoverPasswordRequest> context)
    {
        var request = context.Message;
        
        _logger.LogInformation("========================================");
        _logger.LogInformation($"[KAFKA AUTH] Solicitud de recuperación recibida para: {request.Email}");
        
        var tokenGenerado = Guid.NewGuid().ToString("N");
        var evento = new RecoveryMessages.PasswordRecoveryRequestedEvent(request.Email, tokenGenerado);
        
        await _eventProducer.Produce(evento);
        _logger.LogInformation($"[KAFKA AUTH] Token temporal generado: {tokenGenerado}");
        _logger.LogInformation($"[KAFKA AUTH] Evento 'PasswordRecoveryRequestedEvent' disparado exitosamente.");
        _logger.LogInformation("========================================");
        
    }
    
    
    
    
}