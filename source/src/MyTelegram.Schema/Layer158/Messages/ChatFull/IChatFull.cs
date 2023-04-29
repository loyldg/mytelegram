// ReSharper disable All

namespace MyTelegram.Schema.Messages;

public interface IChatFull : IObject
{
    Schema.IChatFull FullChat { get; set; }
    TVector<Schema.IChat> Chats { get; set; }
    TVector<Schema.IUser> Users { get; set; }
}
