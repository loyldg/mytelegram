namespace MyTelegram.Domain.Commands.Channel;

public class EditChannelAdminCommand : RequestCommand2<ChannelAggregate, ChannelId, IExecutionResult>
{
    public EditChannelAdminCommand(ChannelId aggregateId,
        RequestInfo requestInfo,
        long promotedBy,
        bool canEdit,
        long userId,
        bool isBot,
        ChatAdminRights adminRights,
        string rank,
        int date) : base(aggregateId, requestInfo)
    {
        PromotedBy = promotedBy;
        CanEdit = canEdit;
        UserId = userId;
        IsBot = isBot;
        AdminRights = adminRights;
        Rank = rank;
        Date = date;
    }

    public ChatAdminRights AdminRights { get; }
    public bool CanEdit { get; }
    public long PromotedBy { get; }
    public string Rank { get; }
    public int Date { get; }
    public long UserId { get; }
    public bool IsBot { get; }
}