//namespace MyTelegram.Domain.CommandHandlers.Message;

//public class EditInboxCommandHandler : CommandHandler<MessageBoxAggregate, MessageBoxId, EditInboxCommand>
//{
//    public override Task ExecuteAsync(MessageBoxAggregate aggregate,
//        EditInboxCommand command,
//        CancellationToken cancellationToken)
//    {
//        aggregate.EditInbox( /*command.ReqMsgId, */command.InboxOwnerPeerId,
//            command.MessageId,
//            command.NewMessage,
//            command.Entities,
//            command.Date,
//            command.Media,
//            command.CorrelationId);
//        return Task.CompletedTask;
//    }
//}


