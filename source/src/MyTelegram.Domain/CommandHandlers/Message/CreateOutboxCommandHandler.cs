//namespace MyTelegram.Domain.CommandHandlers.Message;

//public class CreateOutboxCommandHandler : CommandHandler<MessageBoxAggregate, MessageBoxId, CreateOutboxCommand>
//{
//    public override Task ExecuteAsync(MessageBoxAggregate aggregate,
//        CreateOutboxCommand command,
//        CancellationToken cancellationToken)
//    {
//        var ownerPeerId = command.ToPeerType == PeerType.Channel ? command.ToPeerId : command.OwnerPeerId;

//        aggregate.CreateOutbox(command.ReqMsgId,
//            command.SenderAuthKeyId,
//            command.SenderPermAuthKeyId,
//            //command.GlobalMessageId,
//            //command.DialogId,
//            //command.OwnerPeerId,
//            ownerPeerId,
//            command.SenderPeerId,
//            command.ToPeerType,
//            command.ToPeerId,
//            command.MessageId,
//            //command.Pts,
//            //command.Out,
//            command.Date,
//            command.Message,
//            //command.MessageData,
//            command.Entities,
//            command.SendMessageType,
//            command.MessageBoxType,
//            command.RandomId,
//            command.ReplyToMsgId,
//            command.OwnerIsBot,
//            command.ReceiverOwnerIsBot,
//            command.MessageActionData,
//            command.CorrelationId,
//            command.SubType,
//            command.FwdHeader,
//            command.MessageActionType,
//            command.ClearDraft,
//            command.Media,
//            command.GroupId,
//            command.GroupItemCount,
//            command.Views
//        );

//        return Task.CompletedTask;
//    }
//}
