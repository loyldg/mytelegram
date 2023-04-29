// ReSharper disable All

namespace MyTelegram.Schema.Chatlists;

public interface IExportedInvites : IObject
{
    TVector<Schema.IExportedChatlistInvite> Invites { get; set; }
    TVector<Schema.IChat> Chats { get; set; }
    TVector<Schema.IUser> Users { get; set; }
}
