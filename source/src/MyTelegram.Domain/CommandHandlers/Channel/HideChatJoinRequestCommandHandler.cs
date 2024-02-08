namespace MyTelegram.Domain.CommandHandlers.Channel;

public class HideChatJoinRequestCommandHandler : CommandHandler<ChannelAggregate, ChannelId, HideChatJoinRequestCommand>
{
    public override Task ExecuteAsync(ChannelAggregate aggregate, HideChatJoinRequestCommand command, CancellationToken cancellationToken)
    {
        aggregate.HideChatJoinRequest(command.RequestInfo, command.UserId, command.Approved);
        return Task.CompletedTask;
    }
}