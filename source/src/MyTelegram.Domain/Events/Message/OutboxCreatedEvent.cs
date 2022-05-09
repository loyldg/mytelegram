// namespace MyTelegram.Domain.Events.Message;

// public class OutboxCreatedEvent : RequestAggregateEvent<MessageBoxAggregate, MessageBoxId>, IHasCorrelationId
// {
//     public OutboxCreatedEvent(
//         long reqMsgId,
//         long senderAuthKeyId,
//         long senderPermAuthKeyId,
//         //long globalMessageId,
//         //DialogId dialogId,
//         //UserId ownerId,
//         //UserId senderId,
//         long ownerPeerId,
//         long senderPeerId,
//         PeerType toPeerType,
//         long toPeerId,
//         int messageId,
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
//         bool receiverOwnerIsBot,
//         string? messageActionData,
//         //bool post,
//         Guid correlationId,
//         MessageBoxSubType subType,
//         MessageFwdHeader? fwdHeader,
//         MessageActionType messageActionType,
//         bool clearDraft,
//         byte[]? media,
//         long? groupId,
//         int groupItemCount,
//         int? views
//         //int deletedChatUserId
//     ) : base(reqMsgId)
//     {
//         //OwnerId = ownerId;
//         //SenderId = senderId;
//         //ReqMsgId = reqMsgId;
//         //GlobalMessageId = globalMessageId;
//         SenderAuthKeyId = senderAuthKeyId;
//         SenderPermAuthKeyId = senderPermAuthKeyId;
//         OwnerPeerId = ownerPeerId;
//         SenderPeerId = senderPeerId;
//         ToPeerType = toPeerType;
//         ToPeerId = toPeerId;
//         MessageId = messageId;
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
//         SubType = subType;
//         FwdHeader = fwdHeader;
//         MessageActionType = messageActionType;
//         ClearDraft = clearDraft;
//         Media = media;
//         GroupId = groupId;
//         GroupItemCount = groupItemCount;
//         Views = views;
//         ReplyToMsgId = replyToMsgId;
//         OwnerIsBot = ownerIsBot;
//         ReceiverOwnerIsBot = receiverOwnerIsBot;
//         MessageActionData = messageActionData;
//         //Post = post;
//         //DialogId = dialogId;
//     }

//     public bool ClearDraft { get; }

//     //public int Pts { get; }
//     //public bool Out { get; }
//     public int Date { get; }

//     //public MessageData MessageData { get; }
//     public byte[]? Entities { get; }
//     public MessageFwdHeader? FwdHeader { get; }
//     public long? GroupId { get; }
//     public int GroupItemCount { get; }
//     public byte[]? Media { get; }
//     public string Message { get; }
//     public string? MessageActionData { get; }
//     public MessageActionType MessageActionType { get; }

//     public MessageBoxType MessageBoxType { get; }
//     public int MessageId { get; }
//     public bool OwnerIsBot { get; }
//     public long OwnerPeerId { get; }
//     //public bool Post { get; }
//     public long RandomId { get; }
//     public bool ReceiverOwnerIsBot { get; }
//     public int? ReplyToMsgId { get; }

//     //public DialogId DialogId { get; }
//     //public UserId OwnerId { get; }
//     //public UserId SenderId { get; }

//     //public long ReqMsgId { get; }
//     //public long GlobalMessageId { get; }
//     public long SenderAuthKeyId { get; }
//     public long SenderPermAuthKeyId { get; }
//     public long SenderPeerId { get; }
//     public SendMessageType SendMessageType { get; }
//     public MessageBoxSubType SubType { get; }
//     public long ToPeerId { get; }

//     public PeerType ToPeerType { get; }
//     public int? Views { get; }
//     public Guid CorrelationId { get; }
// }
