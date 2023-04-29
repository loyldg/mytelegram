// using EventFlow.Aggregates.ExecutionResults;
// using MyTelegram.Domain.Events;

// namespace MyTelegram.Domain.Commands.Message;

// public class StartForwardMessageCommand : RequestCommand<MessageBoxAggregate, MessageBoxId, IExecutionResult>,
//     IHasCorrelationId
// {
//     public StartForwardMessageCommand(MessageBoxId aggregateId,
//         long reqMsgId,
//         long selfAuthKeyId,
//         long selfPermAuthKeyId,
//         long selfUserId,
//         Peer fromPeer,
//         Peer toPeer,
//         IReadOnlyList<int> idList,
//         IReadOnlyList<long> randomIdList,
//         Guid correlationId) : base(aggregateId, reqMsgId)
//     {
//         SelfAuthKeyId = selfAuthKeyId;
//         SelfPermAuthKeyId = selfPermAuthKeyId;
//         SelfUserId = selfUserId;
//         FromPeer = fromPeer;
//         ToPeer = toPeer;
//         IdList = idList;
//         RandomIdList = randomIdList;
//         CorrelationId = correlationId;
//     }

//     public Peer FromPeer { get; }
//     public IReadOnlyList<int> IdList { get; }
//     public IReadOnlyList<long> RandomIdList { get; }

//     public long SelfAuthKeyId { get; }
//     public long SelfPermAuthKeyId { get; }
//     public long SelfUserId { get; }
//     public Peer ToPeer { get; }
//     public Guid CorrelationId { get; }

//     protected override IEnumerable<byte[]> GetSourceIdComponents()
//     {
//         yield return CorrelationId.ToByteArray();
//     }
// }

// // 和DeleteMessageCommand功能一样，只是为了不让DeleteMessageSaga也触发相同的事件（提高性能）,所以单独加一个命令

// //public class CreateInboxMessageBoxCommand : CreateMessageBoxCommand
// //{
// //    public CreateInboxMessageBoxCommand(MessageBoxId aggregateId,
// //        UserId ownerId,
// //        UserId senderId,
// //        PeerType toPeerType,
// //        long toPeerId,
// //        int messageId,
// //        int pts,
// //        bool @out,
// //        MessageData messageData,
// //        long randomId,
// //        MessageBoxType messageBoxType) : base(aggregateId, ownerId, senderId, toPeerType, toPeerId, messageId, pts, @out, messageData, randomId, messageBoxType)
// //    {
// //    }
// //}

// //public class StartIncrementViewsCommand : RequestCommand<MessageBoxAggregate, MessageBoxId, IExecutionResult>
// //{
// //    public StartIncrementViewsCommand(MessageBoxId aggregateId,
// //        long reqMsgId,
// //        List<int> messageIdList) : base(aggregateId, reqMsgId)
// //    {
// //        MessageIdList = messageIdList;
// //    }

// //    public List<int> MessageIdList { get; }
// //}

// //public class StartEditMessageCommand : RequestCommand<MessageBoxAggregate, MessageBoxId, IExecutionResult>,IHasCorrelationId
// //{
// //    public StartEditMessageCommand(MessageBoxId aggregateId,
// //        long reqMsgId,
// //        long userId,
// //        int messageId,
// //        string newMessage,
// //        byte[] entities,
// //        Guid correlationId) : base(aggregateId, reqMsgId)
// //    {
// //        UserId = userId;
// //        MessageId = messageId;
// //        NewMessage = newMessage;
// //        Entities = entities;
// //        CorrelationId = correlationId;
// //    }

// //    public long UserId { get; }

// //    public int MessageId { get; }
// //    public string NewMessage { get; }
// //    public byte[] Entities { get; }
// //    public Guid CorrelationId { get; }
// //}
// //public class EditMessageCommand : RequestCommand<MessageBoxAggregate, MessageBoxId, IExecutionResult>,IHasCorrelationId
// //{
// //    public EditMessageCommand(MessageBoxId aggregateId,
// //        long reqMsgId,
// //        long userId,
// //        int messageId,
// //        string newMessage,
// //        byte[] entities,
// //        Guid correlationId) : base(aggregateId, reqMsgId)
// //    {
// //        UserId = userId;
// //        MessageId = messageId;
// //        NewMessage = newMessage;
// //        Entities = entities;
// //        CorrelationId = correlationId;
// //    }

// //    public long UserId { get; }

// //    public int MessageId { get; }
// //    public string NewMessage { get; }
// //    public byte[] Entities { get; }
// //    public Guid CorrelationId { get; }
// //}

// //public class CreateMessageBoxCommand : DistinctCommand<MessageBoxAggregate, MessageBoxId, IExecutionResult>
// //{
// //    public CreateMessageBoxCommand(
// //        MessageBoxId aggregateId,
// //        DialogId dialogId,
// //        //UserId ownerId,
// //        //UserId senderId,
// //        long ownerPeerId,
// //        long senderPeerId,
// //        PeerType toPeerType,
// //        long toPeerId,
// //        int messageId,
// //        int pts,
// //        bool @out,
// //        int date,
// //        MessageData messageData,
// //        long randomId,
// //        MessageBoxType messageBoxType,
// //        int replyToMsgId,
// //        Guid correlationId) : base(aggregateId)
// //    {
// //        DialogId = dialogId;
// //        //OwnerId = ownerId;
// //        //SenderId = senderId;
// //        OwnerPeerId = ownerPeerId;
// //        SenderPeerId = senderPeerId;
// //        ToPeerType = toPeerType;
// //        ToPeerId = toPeerId;
// //        MessageId = messageId;
// //        Pts = pts;
// //        Out = @out;
// //        Date = date;
// //        MessageData = messageData;
// //        RandomId = randomId;
// //        MessageBoxType = messageBoxType;
// //        ReplyToMsgId = replyToMsgId;
// //        CorrelationId = correlationId;
// //    }

// //    public DialogId DialogId { get; }
// //    //public UserId OwnerId { get; }
// //    //public UserId SenderId { get; }
// //    public long OwnerPeerId { get; }
// //    public long SenderPeerId { get; }
// //    public PeerType ToPeerType { get; }
// //    public long ToPeerId { get; }
// //    public int MessageId { get; }
// //    public int Pts { get; }
// //    public bool Out { get; }
// //    public int Date { get; }
// //    public MessageData MessageData { get; }
// //    public long RandomId { get; }
// //    public MessageBoxType MessageBoxType { get; }
// //    public int ReplyToMsgId { get; }
// //    public Guid CorrelationId { get; }

// //    protected override IEnumerable<byte[]> GetSourceIdComponents()
// //    {
// //        yield return BitConverter.GetBytes(ToPeerId);
// //        yield return BitConverter.GetBytes(RandomId);
// //    }
// //}

// //public class SendMessageCommand : DistinctCommand<MessageBoxAggregate, MessageBoxId, IExecutionResult>
// //{
// //    public SendMessageCommand(MessageBoxId aggregateId,
// //        //UserId senderId,
// //        //UserId ownerId,
// //        //UserId receiverId,
// //        long senderPeerId,
// //        long receiverUid,
// //        PeerType toPeerType,
// //        long toPeerId,
// //        string message,
// //        long randomId,
// //        MessageBoxType messageBoxType) : base(aggregateId)
// //    {
// //        //SenderId = senderId;
// //        //ReceiverId = receiverId;
// //        //OwnerId = ownerId;
// //        SenderPeerId = senderPeerId;
// //        ReceiverUid = receiverUid;
// //        ToPeerType = toPeerType;
// //        ToPeerId = toPeerId;
// //        Message = message;
// //        RandomId = randomId;
// //        MessageBoxType = messageBoxType;
// //    }

// //    public long SenderPeerId { get; }
// //    public long ReceiverUid { get; }
// //    //public UserId OwnerId { get; }
// //    public long RandomId { get; }
// //    public string Message { get; }
// //    public long ToPeerId { get; }
// //    public PeerType ToPeerType { get; }
// //    public MessageBoxType MessageBoxType { get; }

// //    protected override IEnumerable<byte[]> GetSourceIdComponents()
// //    {
// //        yield return BitConverter.GetBytes(RandomId);
// //    }
// //}


