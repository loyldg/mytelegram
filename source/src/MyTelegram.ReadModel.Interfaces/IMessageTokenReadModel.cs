namespace MyTelegram.ReadModel.Interfaces;

public interface IMessageTokenReadModel : IReadModel
{
    long OwnerPeerId { get; }
    PeerType ToPeerType { get; }
    long ToPeerId { get; }
    int MessageId { get; }
    long SenderUserId { get; }
    Peer? SavedPeerId { get; }
    int? TopMsgId { get; }
    int? ReplyToMsgId { get; }
    MessageType MessageType { get; }
    bool Pinned { get; }
    bool Post { get; }
    int Date { get; }
    bool PublicPosts { get; }
    List<string> Hashtags { get; }
    List<long> Tokens { get; }
}