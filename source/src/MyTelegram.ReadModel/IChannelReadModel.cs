namespace MyTelegram.ReadModel;

public interface IChannelReadModel : IReadModel
{
    string? About { get; }
    long AccessHash { get; }
    string? Address { get; }

    List<ChatAdmin> AdminList { get; }

    bool Broadcast { get; }

    long ChannelId { get; }
    long CreatorId { get; }

    int Date { get; }

    ChatBannedRights? DefaultBannedRights { get; }
    string Id { get; }
    int LastSendDate { get; }

    long LastSenderPeerId { get; }
    bool MegaGroup { get; }
    int? ParticipantsCount { get; }

    byte[]? Photo { get; }
    int Pts { get; }
    bool Signatures { get; }
    bool SlowModeEnabled { get; }
    string Title { get; }

    //string TopMessageBoxId { get; }
    int TopMessageId { get; }
    string? UserName { get; }
    bool Verified { get; }
    long? LinkedChatId { get; }
}
