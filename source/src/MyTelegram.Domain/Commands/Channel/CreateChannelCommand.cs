namespace MyTelegram.Domain.Commands.Channel;

public class CreateChannelCommand : RequestCommand2<ChannelAggregate, ChannelId, IExecutionResult>, IHasCorrelationId
{
    public CreateChannelCommand(ChannelId aggregateId,
        RequestInfo request,
        long channelId,
        long creatorId,
        string title,
        bool broadcast,
        bool megaGroup,
        string? about,
        string? address,
        long accessHash,
        int date,
        long randomId,
        string messageActionData,
        Guid correlationId) : base(aggregateId, request)
    {
        ChannelId = channelId;
        CreatorId = creatorId;
        Title = title;
        Broadcast = broadcast;
        MegaGroup = megaGroup;
        About = about;
        Address = address;
        AccessHash = accessHash;
        Date = date;
        RandomId = randomId;
        MessageActionData = messageActionData;
        CorrelationId = correlationId;
    }

    public string? About { get; }
    public long AccessHash { get; }
    public string? Address { get; }

    public bool Broadcast { get; }
    public long ChannelId { get; }
    public long CreatorId { get; }
    public int Date { get; }
    public bool MegaGroup { get; }
    public string MessageActionData { get; }
    public long RandomId { get; }
    public string Title { get; }

    public Guid CorrelationId { get; }
}
