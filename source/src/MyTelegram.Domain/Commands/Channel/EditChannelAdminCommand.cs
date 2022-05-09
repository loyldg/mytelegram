namespace MyTelegram.Domain.Commands.Channel;

public class EditChannelAdminCommand : RequestCommand<ChannelAggregate, ChannelId, IExecutionResult>, IHasCorrelationId
{
    public EditChannelAdminCommand(ChannelId aggregateId,
        long reqMsgId,
        long selfUserId,
        long promotedBy,
        bool canEdit,
        long userId,
        ChatAdminRights adminRights,
        string rank,
        Guid correlationId) : base(aggregateId, reqMsgId)
    {
        SelfUserId = selfUserId;
        PromotedBy = promotedBy;
        CanEdit = canEdit;
        UserId = userId;
        AdminRights = adminRights;
        Rank = rank;
        CorrelationId = correlationId;
    }

    public ChatAdminRights AdminRights { get; }
    public bool CanEdit { get; }
    public long PromotedBy { get; }
    public string Rank { get; }

    public long SelfUserId { get; }
    public long UserId { get; }
    public Guid CorrelationId { get; }
}
