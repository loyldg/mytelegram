namespace MyTelegram.Domain.CommandHandlers.Channel;

public class EditBannedCommandHandler : CommandHandler<ChannelMemberAggregate, ChannelMemberId, EditBannedCommand>
{
    public override Task ExecuteAsync(ChannelMemberAggregate aggregate,
        EditBannedCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.EditBanned(command.ReqMsgId,
            command.AdminId,
            command.ChannelId,
            command.MemberUid,
            command.ChatBannedRights);
        return Task.CompletedTask;
    }
}
