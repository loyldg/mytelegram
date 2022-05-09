namespace MyTelegram.Domain.CommandHandlers.Channel;

public class EditChannelAdminCommandHandler : CommandHandler<ChannelAggregate, ChannelId, EditChannelAdminCommand>
{
    public override Task ExecuteAsync(ChannelAggregate aggregate,
        EditChannelAdminCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.EditAdmin(command.ReqMsgId,
            command.SelfUserId,
            command.PromotedBy,
            command.CanEdit,
            command.UserId,
            command.AdminRights,
            command.Rank);
        return Task.CompletedTask;
    }
}
