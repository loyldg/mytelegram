//namespace MyTelegram.Domain.CommandHandlers.Message;

//public class DeleteMessageCommandHandler : CommandHandler<MessageBoxAggregate, MessageBoxId, DeleteMessageCommand>
//{
//    public override Task ExecuteAsync(MessageBoxAggregate aggregate,
//        DeleteMessageCommand command,
//        CancellationToken cancellationToken)
//    {
//        aggregate.DeleteMessage(command.CorrelationId);
//        return Task.CompletedTask;
//    }
//}
