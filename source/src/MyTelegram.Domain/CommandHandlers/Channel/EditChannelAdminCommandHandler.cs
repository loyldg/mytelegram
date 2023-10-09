namespace MyTelegram.Domain.CommandHandlers.Channel;

public class EditChannelAdminCommandHandler : CommandHandler<ChannelAggregate, ChannelId, EditChannelAdminCommand>
{
    public override Task ExecuteAsync(ChannelAggregate aggregate,
        EditChannelAdminCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.EditAdmin(command.RequestInfo,
            command.PromotedBy,
            command.CanEdit,
            command.UserId,
            command.IsBot,
            command.AdminRights,
            command.Rank,
            command.Date);
        return Task.CompletedTask;
    }
}