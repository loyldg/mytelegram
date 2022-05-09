namespace MyTelegram.Domain.CommandHandlers.Channel;

public class
    UpdateChannelUserNameCommandHandler : CommandHandler<ChannelAggregate, ChannelId, UpdateChannelUserNameCommand>
{
    public override Task ExecuteAsync(ChannelAggregate aggregate,
        UpdateChannelUserNameCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.UpdateUserName(command.ReqMsgId, command.UserName, command.CorrelationId);
        return Task.CompletedTask;
    }
}
