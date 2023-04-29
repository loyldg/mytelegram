namespace MyTelegram.Domain.Aggregates.AppCode;

public class AppCodeState : AggregateState<AppCodeAggregate, AppCodeId, AppCodeState>,
    IApply<AppCodeCreatedEvent>,
    IApply<AppCodeCanceledEvent>,
    IApply<SignUpRequiredEvent>,
    IApply<AppCodeCheckFailedEvent>,
    IApply<CheckSignUpCodeCompletedEvent>,
    IApply<CheckSignInCodeCompletedEvent>
{
    public bool Canceled { get; private set; }
    public int Expire { get; private set; }
    public int FailedCount { get; private set; }
    public string PhoneCodeHash { get; private set; } = default!;
    public string Code { get; private set; } = default!;

    public void Apply(AppCodeCanceledEvent aggregateEvent)
    {
        Canceled = true;
    }

    public void Apply(AppCodeCheckFailedEvent aggregateEvent)
    {
        FailedCount++;
    }

    public void Apply(AppCodeCreatedEvent aggregateEvent)
    {
        PhoneCodeHash = aggregateEvent.PhoneCodeHash;
        Code = aggregateEvent.Code;
        Expire = aggregateEvent.Expire;
    }

    public void Apply(CheckSignInCodeCompletedEvent aggregateEvent)
    {
        if (!aggregateEvent.IsCodeValid)
        {
            FailedCount++;
        }
    }

    public void Apply(CheckSignUpCodeCompletedEvent aggregateEvent)
    {
        if (!aggregateEvent.IsCodeValid)
        {
            FailedCount++;
        }
    }

    public void Apply(SignUpRequiredEvent aggregateEvent)
    {
    }
}
