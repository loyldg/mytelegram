// ReSharper disable All

namespace MyTelegram.Schema.Messages;

public interface IInactiveChats : IObject
{
    TVector<int> Dates { get; set; }
    TVector<Schema.IChat> Chats { get; set; }
    TVector<Schema.IUser> Users { get; set; }
}
