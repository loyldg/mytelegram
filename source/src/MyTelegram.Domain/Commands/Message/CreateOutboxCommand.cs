// using EventFlow.Aggregates.ExecutionResults;

// namespace MyTelegram.Domain.Commands.Message;

// public class CreateOutboxCommand : RequestCommand<MessageBoxAggregate, MessageBoxId, IExecutionResult>
// {
//     public CreateOutboxCommand(
//         long reqMsgId,
//         long senderAuthKeyId,
//         long senderPermAuthKeyId,
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
//         bool receiverOwnerIsBot,
//         string? messageActionData,
//         Guid correlationId,
//         MessageActionType messageActionType,
//         MessageBoxSubType subType = MessageBoxSubType.Normal,
//         MessageFwdHeader? fwdHeader = null,
//         bool clearDraft = false,
//         byte[]? media = null,
//         long? groupId = null,
//         int groupItemCount = 0,
//         int? views = null
//         //int deletedChatUserId = 0
//     ) : base(aggregateId, reqMsgId)
//     {
//         //GlobalMessageId = globalMessageId;
//         SenderAuthKeyId = senderAuthKeyId;
//         SenderPermAuthKeyId = senderPermAuthKeyId;
//         //DialogId = dialogId;
//         //OwnerId = ownerId;
//         //SenderId = senderId;
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
//         Media = media;
//         GroupId = groupId;
//         GroupItemCount = groupItemCount;
//         Views = views;
//         RandomId = randomId;
//         SendMessageType = sendMessageType;
//         MessageBoxType = messageBoxType;
//         ReplyToMsgId = replyToMsgId;
//         OwnerIsBot = ownerIsBot;
//         ReceiverOwnerIsBot = receiverOwnerIsBot;
//         MessageActionData = messageActionData;
//         CorrelationId = correlationId;
//         MessageActionType = messageActionType;
//         SubType = subType;
//         FwdHeader = fwdHeader;
//         ClearDraft = clearDraft;
//         //DeletedChatUserId = deletedChatUserId;
//     }

//     public bool ClearDraft { get; }

//     public Guid CorrelationId { get; }

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

//     //public DialogId DialogId { get; }
//     //public UserId OwnerId { get; }
//     //public UserId SenderId { get; }
//     public long OwnerPeerId { get; }
//     public long RandomId { get; }
//     public bool ReceiverOwnerIsBot { get; }
//     public int? ReplyToMsgId { get; }

//     //public long GlobalMessageId { get; }

//     public long SenderAuthKeyId { get; }
//     public long SenderPermAuthKeyId { get; }
//     public long SenderPeerId { get; }
//     public SendMessageType SendMessageType { get; }
//     public MessageBoxSubType SubType { get; }
//     public long ToPeerId { get; }
//     public PeerType ToPeerType { get; }

//     public int? Views { get; }
//     //public int DeletedChatUserId { get; }

//     protected override IEnumerable<byte[]> GetSourceIdComponents()
//     {
//         yield return BitConverter.GetBytes(ToPeerId);
//         yield return BitConverter.GetBytes(RandomId);
//     }
// }
