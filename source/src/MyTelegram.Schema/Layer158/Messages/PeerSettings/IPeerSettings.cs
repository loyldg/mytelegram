// ReSharper disable All

namespace MyTelegram.Schema.Messages;

public interface IPeerSettings : IObject
{
    Schema.IPeerSettings Settings { get; set; }
    TVector<Schema.IChat> Chats { get; set; }
    TVector<Schema.IUser> Users { get; set; }
}
