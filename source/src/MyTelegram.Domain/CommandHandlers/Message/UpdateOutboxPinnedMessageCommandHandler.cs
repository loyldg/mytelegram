//namespace MyTelegram.Domain.CommandHandlers.Message;

//public class
//    UpdateOutboxPinnedMessageCommandHandler : CommandHandler<MessageBoxAggregate, MessageBoxId,
//        UpdateOutboxPinnedMessageCommand>
//{
//    public override Task ExecuteAsync(MessageBoxAggregate aggregate,
//        UpdateOutboxPinnedMessageCommand command,
//        CancellationToken cancellationToken)
//    {
//        aggregate.UpdateOutboxPinnedMessage(command.Pinned,
//            command.PmOneSide,
//            command.Silent,
//            command.Date,
//            command.CorrelationId);
//        return Task.CompletedTask;
//    }
//}
