namespace MyTelegram.Domain.CommandHandlers.Channel;

public class EditChannelAboutCommandHandler : CommandHandler<ChannelAggregate, ChannelId, EditChannelAboutCommand>
{
    public override Task ExecuteAsync(ChannelAggregate aggregate,
        EditChannelAboutCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.EditAbout(command.ReqMsgId, command.SelfUserId, command.About);
        return Task.CompletedTask;
    }
}
