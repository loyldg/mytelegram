// namespace MyTelegram.Domain.Events.Message;

// public class InboxCreatedEvent : AggregateEvent<MessageBoxAggregate, MessageBoxId>, IHasCorrelationId
// {
//     public InboxCreatedEvent(
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
//         SendMessageType sendMessageType,
//         MessageBoxType messageBoxType,
//         long randomId,
//         int? replyToMsgId,
//         bool ownerIsBot,
//         bool senderIsBot,
//         string? messageActionData,
//         Guid correlationId,
//         MessageFwdHeader? fwdHeader,
//         MessageActionType messageActionType,
//         byte[]? media,
//         long? groupId,
//         int? views
//         //string title
//     )
//     {
//         //OwnerId = ownerId;
//         //SenderId = senderId;
//         //GlobalMessageId = globalMessageId;
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
//         SendMessageType = sendMessageType;
//         MessageBoxType = messageBoxType;
//         RandomId = randomId;
//         CorrelationId = correlationId;
//         FwdHeader = fwdHeader;
//         MessageActionType = messageActionType;
//         Media = media;
//         GroupId = groupId;
//         Views = views;
//         //Title = title;
//         ReplyToMsgId = replyToMsgId;
//         OwnerIsBot = ownerIsBot;
//         SenderIsBot = senderIsBot;
//         MessageActionData = messageActionData;
//         //DialogId = dialogId;
//     }

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

//     //public DialogId DialogId { get; }
//     //public UserId OwnerId { get; }
//     //public UserId SenderId { get; }

//     //public long GlobalMessageId { get; }
//     public long OwnerPeerId { get; }
//     public long RandomId { get; }
//     public int? ReplyToMsgId { get; }
//     public bool SenderIsBot { get; }
//     public int SenderMessageId { get; }
//     public long SenderPeerId { get; }
//     public SendMessageType SendMessageType { get; }
//     public long ToPeerId { get; }

//     public PeerType ToPeerType { get; }

//     public int? Views { get; }

//     public Guid CorrelationId { get; }
//     //public string Title { get; }
// }


