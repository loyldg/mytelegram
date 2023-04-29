// ReSharper disable All

namespace MyTelegram.Schema.Messages;

public interface IChats : IObject
{
    TVector<Schema.IChat> Chats { get; set; }
}
