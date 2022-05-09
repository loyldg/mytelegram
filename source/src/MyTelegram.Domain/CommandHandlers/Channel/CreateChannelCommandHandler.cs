namespace MyTelegram.Domain.CommandHandlers.Channel;

public class CreateChannelCommandHandler : CommandHandler<ChannelAggregate, ChannelId, CreateChannelCommand>
{
    public override Task ExecuteAsync(ChannelAggregate aggregate,
        CreateChannelCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.Create(command.Request,
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
            command.CorrelationId
        );
        return Task.CompletedTask;
    }
}
