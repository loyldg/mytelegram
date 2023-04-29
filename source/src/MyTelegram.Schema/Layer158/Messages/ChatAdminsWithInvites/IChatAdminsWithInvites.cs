// ReSharper disable All

namespace MyTelegram.Schema.Messages;

public interface IChatAdminsWithInvites : IObject
{
    TVector<Schema.IChatAdminWithInvites> Admins { get; set; }
    TVector<Schema.IUser> Users { get; set; }
}
