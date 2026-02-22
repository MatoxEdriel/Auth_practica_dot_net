using Auth.Api.Consumers;
using Intercore.shared.Constans.KAFKA.topics;
using Intercore.shared.DTOs.Auth;

namespace Auth.Api.Extensions;
using MassTransit;

public static class KafkaConsumerExtensions
{

    public static void AddAuthConsumers(this IRiderRegistrationConfigurator rider, string consumerGroup, string kafkaHost)
    {
        rider.AddConsumer<RegisterUserConsumer>();
        rider.AddConsumer<RecoverPasswordConsumer>();
        
        rider.AddProducer<RecoveryMessages.PasswordRecoveryRequestedEvent>(AuthTopics.PasswordRecoveryRequested);
        rider.UsingKafka((context, k) =>
        {
            k.Host(kafkaHost);

            k.TopicEndpoint<RegisterMessages.RegisterRequest>(
                AuthTopics.RegisterUserCommand,
                consumerGroup,
                e => { e.ConfigureConsumer<RegisterUserConsumer>(context); });
            
            k.TopicEndpoint<RecoveryMessages.RecoverPasswordRequest>(
                AuthTopics.RecoverPasswordCommand,
                consumerGroup, 
                e => { e.ConfigureConsumer<RecoverPasswordConsumer>(context); });
        });
        
    }
    
}