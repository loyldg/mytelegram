﻿namespace MyTelegram.Domain.Events.Channel;

public class CheckChannelStateCompletedEvent : RequestAggregateEvent2<ChannelAggregate, ChannelId>
{
    public long SenderPeerId { get; }
    public int MessageId { get; }
    public int Date { get; }
    public bool Post { get; }
    public int? Views { get; }
    public IReadOnlyList<long> BotUidList { get; }
    public long? LinkedChannelId { get; }

    public CheckChannelStateCompletedEvent(
        RequestInfo requestInfo,
        long senderPeerId,
        int messageId,
        int date,
        bool post,
        int? views,
        IReadOnlyList<long> botUidList,
        long? linkedChannelId) : base(requestInfo)
    {
        SenderPeerId = senderPeerId;
        MessageId = messageId;
        Date = date;
        Post = post;
        Views = views;
        BotUidList = botUidList;
        LinkedChannelId = linkedChannelId;
    }
}