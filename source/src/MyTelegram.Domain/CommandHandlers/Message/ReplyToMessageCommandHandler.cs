//namespace MyTelegram.Domain.CommandHandlers.Message;

//public class ReplyToMessageCommandHandler : CommandHandler<MessageBoxAggregate, MessageBoxId, ReplyToMessageCommand>
//{
//    public override Task ExecuteAsync(MessageBoxAggregate aggregate,
//        ReplyToMessageCommand command,
//        CancellationToken cancellationToken)
//    {
//        aggregate.ReplyToMessage(command.UserId, command.CorrelationId);
//        return Task.CompletedTask;
//    }
//}
