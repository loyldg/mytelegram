//namespace MyTelegram.Domain.CommandHandlers.Message;

//public class
//    StartUpdatePinMessageCommandHandler : CommandHandler<MessageBoxAggregate, MessageBoxId,
//        StartUpdatePinMessageCommand>
//{
//    public override Task ExecuteAsync(MessageBoxAggregate aggregate,
//        StartUpdatePinMessageCommand command,
//        CancellationToken cancellationToken)
//    {
//        aggregate.StartUpdatePinnedMessage(command.ReqMsgId,
//            command.SelfAuthKeyId,
//            command.SelfPermAuthKeyId,
//            command.SelfUserId,
//            command.Pinned,
//            command.PmOneSide,
//            command.Silent,
//            command.Date,
//            command.RandomId,
//            command.MessageActionData,
//            command.CorrelationId);
//        return Task.CompletedTask;
//    }
//}
