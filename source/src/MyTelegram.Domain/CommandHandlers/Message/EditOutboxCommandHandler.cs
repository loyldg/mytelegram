//namespace MyTelegram.Domain.CommandHandlers.Message;

//public class EditOutboxCommandHandler : CommandHandler<MessageBoxAggregate, MessageBoxId, EditOutboxCommand>
//{
//    public override Task ExecuteAsync(MessageBoxAggregate aggregate,
//        EditOutboxCommand command,
//        CancellationToken cancellationToken)
//    {
//        aggregate.EditOutbox(command.ReqMsgId,
//            command.SelfAuthKeyId,
//            command.UserId,
//            command.MessageId,
//            command.NewMessage,
//            command.Entities,
//            command.Date,
//            command.Media,
//            command.CorrelationId);
//        return Task.CompletedTask;
//    }
//}


