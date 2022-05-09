namespace MyTelegram.ReadModel;

public interface IMessageReadModel : IReadModel
{
    int Date { get; }
    int EditDate { get; }
    byte[]? Entities { get; }
    MessageFwdHeader? FwdHeader { get; }
    long? GroupedId { get; }
    string Id { get; }

    byte[]? Media { get; }
    string Message { get; }
    string? MessageActionData { get; }
    MessageActionType MessageActionType { get; }
    MessageType MessageType { get; }
    int MessageId { get; }
    bool Out { get; }
    long OwnerPeerId { get; }
    bool Pinned { get; }
    bool Post { get; }
    string? PostAuthor { get; }
    int Pts { get; }
    int? ReplyToMsgId { get; }
    int SenderMessageId { get; }
    long SenderPeerId { get; }
    SendMessageType SendMessageType { get; }
    bool Silent { get; }
    long ToPeerId { get; }
    PeerType ToPeerType { get; }
    int? Views { get; }
}