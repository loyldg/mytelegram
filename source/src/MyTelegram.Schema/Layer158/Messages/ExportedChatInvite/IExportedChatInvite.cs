// ReSharper disable All

namespace MyTelegram.Schema.Messages;

public interface IExportedChatInvite : IObject
{
    Schema.IExportedChatInvite Invite { get; set; }
    TVector<Schema.IUser> Users { get; set; }
}
