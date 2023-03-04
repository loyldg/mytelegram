// ReSharper disable All

namespace MyTelegram.Schema.Messages;

public interface IDiscussionMessage : IObject
{
    BitArray Flags { get; set; }
    TVector<MyTelegram.Schema.IMessage> Messages { get; set; }
    int? MaxId { get; set; }
    int? ReadInboxMaxId { get; set; }
    int? ReadOutboxMaxId { get; set; }
    int UnreadCount { get; set; }
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
