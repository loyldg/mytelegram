namespace MyTelegram.Domain.CommandHandlers.Channel;

public class EditChannelTitleCommandHandler : CommandHandler<ChannelAggregate, ChannelId, EditChannelTitleCommand>
{
    public override Task ExecuteAsync(ChannelAggregate aggregate,
        EditChannelTitleCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.EditTitle(command.Request,
            command.Title,
            command.MessageActionData,
            command.RandomId,
            command.CorrelationId);
        return Task.CompletedTask;
    }
}
