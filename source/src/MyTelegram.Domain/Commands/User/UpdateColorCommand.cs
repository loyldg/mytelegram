namespace MyTelegram.Domain.Commands.User;

public class UpdateUserColorCommand : RequestCommand2<UserAggregate, UserId, IExecutionResult>
{
    public PeerColor? Color { get; }
    public bool ForProfile { get; }

    public UpdateUserColorCommand(UserId aggregateId, RequestInfo requestInfo,
        PeerColor? color,
        bool forProfile
    ) : base(aggregateId, requestInfo)
    {
        Color = color;
        ForProfile = forProfile;
    }
}