//namespace MyTelegram.Domain.CommandHandlers.Message;

//public class
//    AddInboxMessageIdToOutboxCommandHandler : CommandHandler<MessageBoxAggregate, MessageBoxId,
//        AddInboxMessageIdToOutboxCommand>
//{
//    public override Task ExecuteAsync(MessageBoxAggregate aggregate,
//        AddInboxMessageIdToOutboxCommand command,
//        CancellationToken cancellationToken)
//    {
//        aggregate.AddInboxIdToOutbox(command.InboxOwnerPeerId, command.InboxMessageId /*, command.CorrelationId*/);
//        return Task.CompletedTask;
//    }
//}
