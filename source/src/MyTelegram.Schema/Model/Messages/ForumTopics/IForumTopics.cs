// ReSharper disable All

namespace MyTelegram.Schema.Messages;

public interface IForumTopics : IObject
{
    BitArray Flags { get; set; }
    bool OrderByCreateDate { get; set; }
    int Count { get; set; }
    TVector<MyTelegram.Schema.IForumTopic> Topics { get; set; }
    TVector<MyTelegram.Schema.IMessage> Messages { get; set; }
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
    int Pts { get; set; }

}
