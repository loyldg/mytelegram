// ReSharper disable All
namespace MyTelegram.Schema;

public interface ILayeredChat : IChat
{
    bool Deactivated { get; set; }
    bool Creator { get; set; }
    int ParticipantsCount { get; set; }
    IChatBannedRights? DefaultBannedRights { get; set; }
    IChatPhoto Photo { get; set; }
}