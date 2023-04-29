// ReSharper disable All

namespace MyTelegram.Schema.Messages;

public interface IExportedChatInvites : IObject
{
    int Count { get; set; }
    TVector<Schema.IExportedChatInvite> Invites { get; set; }
    TVector<Schema.IUser> Users { get; set; }
}
