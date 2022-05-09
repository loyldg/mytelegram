namespace MyTelegram.Domain.Commands.User;

public class SetSupportCommand : Command<UserAggregate, UserId, IExecutionResult>
{
    public SetSupportCommand(UserId aggregateId,
        bool support) : base(aggregateId)
    {
        Support = support;
    }

    public bool Support { get; }
}
