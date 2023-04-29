//namespace MyTelegram.Domain.CommandHandlers.Message;

//public class CreateInboxCommandHandler : CommandHandler<MessageBoxAggregate, MessageBoxId, CreateInboxCommand>
//{
//    public override Task ExecuteAsync(MessageBoxAggregate aggregate,
//        CreateInboxCommand command,
//        CancellationToken cancellationToken)
//    {
//        aggregate.CreateInbox(
//            //command.GlobalMessageId,
//            //command.DialogId,
//            command.OwnerPeerId,
//            command.SenderPeerId,
//            command.ToPeerType,
//            command.ToPeerId,
//            command.MessageId,
//            command.SenderMessageId,
//            //command.Out, 
//            command.Date,
//            command.Message,
//            command.Entities,
//            command.SendMessageType,
//            command.MessageBoxType,
//            command.RandomId,
//            command.ReplyToMsgId,
//            command.OwnerIsBot,
//            command.SenderIsBot,
//            command.MessageActionData,
//            command.CorrelationId,
//            command.FwdHeader,
//            command.MessageActionType,
//            command.Media,
//            command.GroupId,
//            command.Views
//            //command.Title
//        );

//        return Task.CompletedTask;
//    }
//}


