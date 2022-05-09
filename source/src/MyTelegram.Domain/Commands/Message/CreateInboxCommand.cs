// using EventFlow.Aggregates.ExecutionResults;

// namespace MyTelegram.Domain.Commands.Message;

// public class CreateInboxCommand : /*Distinct*/Command<MessageBoxAggregate, MessageBoxId, IExecutionResult>
// {
//     public CreateInboxCommand(
//         MessageBoxId aggregateId,
//         //long globalMessageId,
//         //DialogId dialogId,
//         //UserId ownerId,
//         //UserId senderId,
//         long ownerPeerId,
//         long senderPeerId,
//         PeerType toPeerType,
//         long toPeerId,
//         int messageId,
//         int senderMessageId,
//         //int pts,
//         //bool @out,
//         int date,
//         //MessageData messageData,
//         string message,
//         byte[]? entities,
//         long randomId,
//         SendMessageType sendMessageType,
//         MessageBoxType messageBoxType,
//         int? replyToMsgId,
//         bool ownerIsBot,
//         bool senderIsBot,
//         string? messageActionData,
//         Guid correlationId,
//         MessageActionType messageActionType,
//         MessageFwdHeader? fwdHeader = null,
//         string? title = null,
//         byte[]? media = null,
//         long? groupId = null,
//         int? views = null
//     ) : base(aggregateId)
//     {
//         //GlobalMessageId = globalMessageId;
//         //DialogId = dialogId;
//         //OwnerId = ownerId;
//         //SenderId = senderId;
//         OwnerPeerId = ownerPeerId;
//         SenderPeerId = senderPeerId;
//         ToPeerType = toPeerType;
//         ToPeerId = toPeerId;
//         MessageId = messageId;
//         SenderMessageId = senderMessageId;
//         //Pts = pts;
//         //Out = @out;
//         Date = date;
//         Message = message;
//         //MessageData = messageData;
//         Entities = entities;
//         RandomId = randomId;
//         SendMessageType = sendMessageType;
//         MessageBoxType = messageBoxType;
//         ReplyToMsgId = replyToMsgId;
//         OwnerIsBot = ownerIsBot;
//         SenderIsBot = senderIsBot;
//         MessageActionData = messageActionData;
//         CorrelationId = correlationId;
//         MessageActionType = messageActionType;
//         FwdHeader = fwdHeader;
//         Title = title;
//         Media = media;
//         GroupId = groupId;
//         Views = views;
//     }

//     public Guid CorrelationId { get; }

//     //public int Pts { get; }
//     //public bool Out { get; }
//     public int Date { get; }

//     //public MessageData MessageData { get; }
//     public byte[]? Entities { get; }
//     public MessageFwdHeader? FwdHeader { get; }
//     public long? GroupId { get; }
//     public byte[]? Media { get; }
//     public string Message { get; }
//     public string? MessageActionData { get; }
//     public MessageActionType MessageActionType { get; }
//     public MessageBoxType MessageBoxType { get; }
//     public int MessageId { get; }
//     public bool OwnerIsBot { get; }

//     //public long GlobalMessageId { get; }

//     //public DialogId DialogId { get; }
//     //public UserId OwnerId { get; }
//     //public UserId SenderId { get; }
//     public long OwnerPeerId { get; }
//     public long RandomId { get; }
//     public int? ReplyToMsgId { get; }
//     public bool SenderIsBot { get; }
//     public int SenderMessageId { get; }
//     public long SenderPeerId { get; }
//     public SendMessageType SendMessageType { get; }
//     public string? Title { get; }
//     public long ToPeerId { get; }
//     public PeerType ToPeerType { get; }
//     public int? Views { get; }

//     //protected override IEnumerable<byte[]> GetSourceIdComponents()
//     //{
//     //    yield return BitConverter.GetBytes(OwnerPeerId);
//     //    yield return BitConverter.GetBytes(RandomId);
//     //}
// }
