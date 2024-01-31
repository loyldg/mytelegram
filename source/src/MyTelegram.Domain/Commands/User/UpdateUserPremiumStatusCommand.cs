namespace MyTelegram.Domain.Commands.User;

public class UpdateUserPremiumStatusCommand : Command<UserAggregate, UserId, IExecutionResult>
{
    public bool Premium { get; }

    public UpdateUserPremiumStatusCommand(UserId aggregateId,bool premium) : base(aggregateId)
    {
        Premium = premium;
    }
}