// ReSharper disable All

namespace MyTelegram.Schema;

public interface ILayeredChannel : IChat
{
    bool Creator { get; set; }
    IChatPhoto Photo { get; set; }
    bool Left { get; set; }
    IChatBannedRights? BannedRights { get; set; }
    IChatAdminRights? AdminRights { get; set; }
    IChatBannedRights? DefaultBannedRights { get; set; }

    int? ParticipantsCount { get; set; }
    //bool Deactivated { get; set; }
}
