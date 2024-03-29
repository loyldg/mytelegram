﻿namespace MyTelegram.Domain.Sagas.Events;

public class ReceiveInboxMessageCompletedEvent : AggregateEvent<SendMessageSaga, SendMessageSagaId>
{
    public MessageItem MessageItem { get; }
    public int Pts { get; }
    public string? ChatTitle { get; }
    public ReceiveInboxMessageCompletedEvent(MessageItem messageItem, int pts, string? chatTitle)
    {
        MessageItem = messageItem;
        Pts = pts;
        ChatTitle = chatTitle;
    }
}