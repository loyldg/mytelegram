// ReSharper disable All

namespace MyTelegram.Schema;

public interface IChatAdminWithInvites : IObject
{
    long AdminId { get; set; }
    int InvitesCount { get; set; }
    int RevokedInvitesCount { get; set; }
}
