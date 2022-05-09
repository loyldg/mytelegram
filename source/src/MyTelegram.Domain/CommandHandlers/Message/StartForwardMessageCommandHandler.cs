//namespace MyTelegram.Domain.CommandHandlers.Message;

//public class
//    StartForwardMessageCommandHandler : CommandHandler<MessageBoxAggregate, MessageBoxId,
//        StartForwardMessageCommand>
//{
//    public override Task ExecuteAsync(MessageBoxAggregate aggregate,
//        StartForwardMessageCommand command,
//        CancellationToken cancellationToken)
//    {
//        aggregate.StartForwardMessage(command.ReqMsgId,
//            command.SelfAuthKeyId,
//            command.SelfPermAuthKeyId,
//            command.SelfUserId,
//            command.FromPeer,
//            command.ToPeer,
//            command.IdList,
//            command.RandomIdList,
//            command.CorrelationId);
//        return Task.CompletedTask;
//    }
//}

////public class
////    StartEditMessageCommandHandler : CommandHandler<MessageBoxAggregate, MessageBoxId, StartEditMessageCommand>
////{
////    public override Task ExecuteAsync(MessageBoxAggregate aggregate,
////        StartEditMessageCommand command,
////        CancellationToken cancellationToken)
////    {
////        aggregate.StartEditMessage(command.ReqMsgId,
////            command.UserId,
////            command.MessageId,
////            command.NewMessage,
////            command.Entities,
////            command.CorrelationId);
////        return Task.CompletedTask;
////    }
////}

////public class EditMessageCommandHandler : CommandHandler<MessageBoxAggregate, MessageBoxId, EditMessageCommand>
////{
////    public override Task ExecuteAsync(MessageBoxAggregate aggregate,
////        EditMessageCommand command,
////        CancellationToken cancellationToken)
////    {
////        aggregate.EditMessage(command.ReqMsgId,command.UserId,command.MessageId,command.NewMessage,command.Entities,command.CorrelationId);
////        return Task.CompletedTask;
////    }
////}

////    public class SendMessageCommandHandler : CommandHandler<MessageBoxAggregate, MessageBoxId, SendMessageCommand>
////    {
////        private readonly IIdGenerator _idGenerator;
////        private readonly ICommandBus _commandBus;

////        public SendMessageCommandHandler(IIdGenerator idGenerator,
////            ICommandBus commandBus)
////        {
////            _idGenerator = idGenerator;
////            _commandBus = commandBus;
////        }

////        public override async Task ExecuteAsync(MessageBoxAggregate aggregate,
////            SendMessageCommand cmd,
////            CancellationToken cancellationToken)
////        {
////            var outboxId = MessageBoxId.Create(cmd.SenderPeerId, cmd.ToPeerId, cmd.RandomId);
////            var inboxId = MessageBoxId.Create(cmd.ReceiverUid, cmd.ToPeerId, cmd.RandomId);
////            var senderDialogId = DialogId.Create(cmd.SenderPeerId, cmd.ToPeerType, cmd.ToPeerId);
////            var receiverDialogId = DialogId.Create(cmd.ReceiverUid, PeerType.User, cmd.SenderPeerId);

////            var data = new MessageData(cmd.Message);
////            var senderPts = await _idGenerator.NextIdAsync(IdType.Pts, cmd.SenderPeerId);
////            var outboxMessageId = await _idGenerator.NextIdAsync(IdType.MessageId, cmd.SenderPeerId);
////            var inboxMessageId = await _idGenerator.NextIdAsync(IdType.MessageId, cmd.ReceiverUid);
////            var date = DateTime.UtcNow.ToTimestamp();
////            var receiverPts = await _idGenerator.NextIdAsync(IdType.Pts, cmd.ReceiverUid);
////            var outboxCommand = new CreateMessageBoxCommand(
////                outboxId,
////                senderDialogId,
////                cmd.SenderPeerId,
////                cmd.SenderPeerId,
////                cmd.ToPeerType,
////                cmd.ToPeerId,
////                outboxMessageId,
////                senderPts,
////                true,
////                date,
////                data,
////                cmd.RandomId,
////                cmd.MessageBoxType);
////            var inboxCommand = new CreateMessageBoxCommand(inboxId, receiverDialogId, cmd.ReceiverUid, cmd.SenderPeerId, cmd.ToPeerType,
////                cmd.ToPeerId, inboxMessageId, receiverPts, false, date, data, cmd.RandomId, cmd.MessageBoxType);
////            await _commandBus.PublishAsync(outboxCommand, CancellationToken.None);
////            await _commandBus.PublishAsync(inboxCommand, CancellationToken.None);

////            //throw new NotImplementedException();
////        }
////    }
