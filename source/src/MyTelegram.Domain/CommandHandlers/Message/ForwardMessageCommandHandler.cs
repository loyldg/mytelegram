//namespace MyTelegram.Domain.CommandHandlers.Message;

//public class ForwardMessageCommandHandler : CommandHandler<MessageBoxAggregate, MessageBoxId, ForwardMessageCommand>
//{
//    public override Task ExecuteAsync(MessageBoxAggregate aggregate,
//        ForwardMessageCommand command,
//        CancellationToken cancellationToken)
//    {
//        aggregate.ForwardMessage(command.SelfAuthKeyId, command.SelfAuthKeyId, command.RandomId, command.CorrelationId);
//        return Task.CompletedTask;
//    }
//}


