// ReSharper disable All

namespace MyTelegram.Schema.Chatlists;

public interface IExportedInvites : IObject
{
    TVector<MyTelegram.Schema.IExportedChatlistInvite> Invites { get; set; }
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
