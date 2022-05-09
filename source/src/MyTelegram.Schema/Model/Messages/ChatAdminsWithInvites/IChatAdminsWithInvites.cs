// ReSharper disable All

namespace MyTelegram.Schema.Messages;

public interface IChatAdminsWithInvites : IObject
{
    TVector<MyTelegram.Schema.IChatAdminWithInvites> Admins { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }

}
