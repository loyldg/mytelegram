//namespace MyTelegram.Domain.CommandHandlers.Message;

//public class
//    StartDeleteMessagesCommandHandler : CommandHandler<MessageBoxAggregate, MessageBoxId,
//        StartDeleteMessagesCommand>
//{
//    public override Task ExecuteAsync(MessageBoxAggregate aggregate,
//        StartDeleteMessagesCommand command,
//        CancellationToken cancellationToken)
//    {
//        aggregate.StartDeleteMessages(command.ReqMsgId,
//            command.SelfAuthKeyId,
//            command.SelfUserId,
//            command.Revoke,
//            command.IdList,
//            command.CorrelationId);
//        return Task.CompletedTask;
//    }
//}
