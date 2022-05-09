// ReSharper disable All

namespace MyTelegram.Schema.Messages;

public interface IInactiveChats : IObject
{
    TVector<int> Dates { get; set; }
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }

}
