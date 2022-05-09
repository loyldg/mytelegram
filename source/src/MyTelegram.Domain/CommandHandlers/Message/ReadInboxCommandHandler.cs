//namespace MyTelegram.Domain.CommandHandlers.Message;

//public class ReadInboxCommandHandler : CommandHandler<MessageBoxAggregate, MessageBoxId, ReadInboxCommand>
//{
//    public override Task ExecuteAsync(MessageBoxAggregate aggregate,
//        ReadInboxCommand command,
//        CancellationToken cancellationToken)
//    {
//        aggregate.ReadInboxMessage(command.ReqMsgId, command.ReaderUid, command.SourceCommandId, command.CorrelationId);
//        return Task.CompletedTask;
//    }
//}
