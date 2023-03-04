// ReSharper disable All

namespace MyTelegram.Schema.Messages;

public interface IChatFull : IObject
{
    MyTelegram.Schema.IChatFull FullChat { get; set; }
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
