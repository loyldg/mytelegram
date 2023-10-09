namespace MyTelegram.Domain.CommandHandlers.Channel;

public class EditChannelTitleCommandHandler : CommandHandler<ChannelAggregate, ChannelId, EditChannelTitleCommand>
{
    public override Task ExecuteAsync(ChannelAggregate aggregate,
        EditChannelTitleCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.EditTitle(command.RequestInfo,
            command.Title,
            command.MessageActionData,
            command.RandomId);
        return Task.CompletedTask;
    }
}