namespace MyTelegram.Domain.CommandHandlers.Channel;

public class UpdateChannelColorCommandHandler : CommandHandler<ChannelAggregate, ChannelId, UpdateChannelColorCommand>
{
    public override Task ExecuteAsync(ChannelAggregate aggregate, UpdateChannelColorCommand command, CancellationToken cancellationToken)
    {
        aggregate.UpdateColor(command.RequestInfo, command.Color, command.BackgroundEmojiId, command.ForProfile);
        return Task.CompletedTask;
    }
}