//namespace MyTelegram.Domain.CommandHandlers.Message;

//public class
//    UpdateInboxPinnedMessageCommandHandler : CommandHandler<MessageBoxAggregate, MessageBoxId,
//        UpdateInboxPinnedMessageCommand>
//{
//    public override Task ExecuteAsync(MessageBoxAggregate aggregate,
//        UpdateInboxPinnedMessageCommand command,
//        CancellationToken cancellationToken)
//    {
//        aggregate.UpdateInboxPinnedMessage(command.Pinned,
//            command.PmOneSide,
//            command.Silent,
//            command.Date,
//            command.CorrelationId);
//        return Task.CompletedTask;
//    }
//}
