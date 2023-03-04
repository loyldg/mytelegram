// ReSharper disable All

namespace MyTelegram.Schema.Messages;

public interface IPeerSettings : IObject
{
    MyTelegram.Schema.IPeerSettings Settings { get; set; }
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
