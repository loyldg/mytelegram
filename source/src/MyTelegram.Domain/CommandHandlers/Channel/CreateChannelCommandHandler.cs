namespace MyTelegram.Domain.CommandHandlers.Channel;

public class CreateChannelCommandHandler : CommandHandler<ChannelAggregate, ChannelId, CreateChannelCommand>
{
    public override Task ExecuteAsync(ChannelAggregate aggregate,
        CreateChannelCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.Create(command.RequestInfo,
            command.ChannelId,
            command.CreatorId,
            command.Broadcast,
            command.MegaGroup,
            command.Title,
            command.About,
            command.Address,
            command.AccessHash,
            command.Date,
            command.RandomId,
            command.MessageActionData,
            command.TtlPeriod,
            command.MigratedFromChat,
            command.MigrateFromChatId,
            command.MigratedMaxId,
            command.PhotoId
        );
        return Task.CompletedTask;
    }
}