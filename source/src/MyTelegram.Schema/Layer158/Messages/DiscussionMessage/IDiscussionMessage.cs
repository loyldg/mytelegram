// ReSharper disable All

namespace MyTelegram.Schema.Messages;

public interface IDiscussionMessage : IObject
{
    BitArray Flags { get; set; }
    TVector<Schema.IMessage> Messages { get; set; }
    int? MaxId { get; set; }
    int? ReadInboxMaxId { get; set; }
    int? ReadOutboxMaxId { get; set; }
    int UnreadCount { get; set; }
    TVector<Schema.IChat> Chats { get; set; }
    TVector<Schema.IUser> Users { get; set; }
}
