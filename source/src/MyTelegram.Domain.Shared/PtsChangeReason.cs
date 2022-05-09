// ReSharper disable once CheckNamespace
namespace MyTelegram;

public enum PtsChangeReason
{
    //SendMessage,
    OutboxCreated = 0,
    InboxCreated = 1,
    ReadInboxMessage = 2,
    OutboxMessageHasRead = 3,
    OutboxEdited = 4,
    InboxEdited = 5,
    OutboxPinnedChanged = 6,
    InboxPinnedChanged = 7,
    //ReadHistory,
    DeleteMessage,
    ClearHistory,
    EditMessage,
    Others,
}