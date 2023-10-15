using MyTelegram.ReadModel.Impl;

namespace MyTelegram.ReadModel.ReadModelLocators;

public interface IChannelReadModelLocator : IReadModelLocator
{
}

public interface IPtsForAuthKeyIdReadModelLocator : IReadModelLocator
{

}

public class PtsForAuthKeyIdReadModelLocator : IPtsForAuthKeyIdReadModelLocator
{
    public IEnumerable<string> GetReadModelIds(IDomainEvent domainEvent)
    {
        var aggregateEvent = domainEvent.GetAggregateEvent();
        var permAuthKeyId = 0L;
        var peerId = 0L;
        switch (aggregateEvent)
        {
            //case PtsAckedEvent ptsAckedEvent:
            //    permAuthKeyId = ptsAckedEvent.PermAuthKeyId;
            //    peerId= ptsAckedEvent.PeerId;
            //    break;
            case PtsUpdatedEvent ptsUpdatedEvent:
                permAuthKeyId = ptsUpdatedEvent.PermAuthKeyId;
                peerId = ptsUpdatedEvent.PeerId;
                break;
        }

        if (permAuthKeyId != 0)
        {
            yield return PtsForAuthKeyId.Create(peerId, permAuthKeyId).Value;
        }
    }
}

public interface IChatInviteReadModelLocator : IReadModelLocator
{
}

public class ChatInviteReadModelLocator : IChatInviteReadModelLocator
{
    public IEnumerable<string> GetReadModelIds(IDomainEvent domainEvent)
    {
        var aggregateEvent = domainEvent.GetAggregateEvent();
        switch (aggregateEvent)
        {
            case ChannelInviteExportedEvent exportChatInviteEvent:
                yield return ChatInviteId.Create(exportChatInviteEvent.ChannelId, exportChatInviteEvent.InviteId).Value;
                break;
            case ChannelInviteEditedEvent exportChatInviteEditedEvent:
                yield return ChatInviteId.Create(exportChatInviteEditedEvent.ChannelId, exportChatInviteEditedEvent.InviteId).Value;
                break;

            case ChannelInviteDeletedEvent channelInviteDeletedEvent:
                yield return ChatInviteId.Create(channelInviteDeletedEvent.ChannelId, channelInviteDeletedEvent.InviteId).Value;
                break;
        }
    }
}