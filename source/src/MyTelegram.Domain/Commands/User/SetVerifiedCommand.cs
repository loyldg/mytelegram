namespace MyTelegram.Domain.Commands.User;

public class SetVerifiedCommand : Command<UserAggregate, UserId, IExecutionResult>
{
    public SetVerifiedCommand(UserId aggregateId,
        bool verified) : base(aggregateId)
    {
        Verified = verified;
    }

    public bool Verified { get; }
}
