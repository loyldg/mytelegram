// ReSharper disable All

namespace MyTelegram.Schema.Messages;

public interface IChats : IObject
{
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }

}
