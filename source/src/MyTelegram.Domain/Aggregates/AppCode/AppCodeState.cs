namespace MyTelegram.Domain.Aggregates.AppCode;

public class AppCodeState : AggregateState<AppCodeAggregate, AppCodeId, AppCodeState>,
    IApply<AppCodeCreatedEvent>,
    IApply<AppCodeCanceledEvent>,
    IApply<SignUpRequiredSagaEvent>,
    //IApply<AppCodeCheckFailedEvent>,
    IApply<CheckSignUpCodeCompletedEvent>,
    IApply<CheckSignInCodeCompletedEvent>//,
    //IApply<CheckAppCodeCompletedEvent>

{
    public bool Canceled { get; private set; }
    public string Code { get; private set; } = default!;
    public string? Email { get; private set; }
    public int Expire { get; private set; }
    public int FailedCount { get; private set; }
    public DateTime LastEmailCodeSendDate { get; private set; }
    public DateTime LastSmsCodeSendDate { get; private set; }
    public string? PhoneNumber { get; private set; }
    public string PhoneCodeHash { get; private set; } = default!;
    public int TodaySentCount { get; private set; }
    public int TotalSentCount { get; private set; }
    public AppCodeType AppCodeType { get; private set; }
    public long UserId { get; private set; }
    public void Apply(AppCodeCanceledEvent aggregateEvent)
    {
        Canceled = true;
    }

    //public void Apply(AppCodeCheckFailedEvent aggregateEvent)
    //{
    //    FailedCount++;
    //}

    public void Apply(AppCodeCreatedEvent aggregateEvent)
    {
        PhoneNumber = aggregateEvent.PhoneNumber;
        PhoneCodeHash = aggregateEvent.PhoneCodeHash;
        Code = aggregateEvent.Code;
        Expire = aggregateEvent.Expire;
        FailedCount = 0;
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

    public void Apply(SignUpRequiredSagaEvent aggregateEvent)
    {
    }

    //public void Apply(CheckAppCodeCompletedEvent aggregateEvent)
    //{
    //    if (!aggregateEvent.IsValidCode)
    //    {
    //        FailedCount++;
    //    }
    //}

    public void LoadSnapshot(AppCodeSnapshot snapshot)
    {
        UserId = snapshot.UserId;
        Expire = snapshot.Expire;
        FailedCount = snapshot.FailedCount;
        PhoneNumber = snapshot.PhoneNumber;
        PhoneCodeHash = snapshot.PhoneCodeHash;
        Code = snapshot.Code;
        Email = snapshot.Email;
        LastSmsCodeSendDate = snapshot.LastSmsCodeSendDate;
        LastEmailCodeSendDate = snapshot.LastEmailCodeSendDate;
        TotalSentCount = snapshot.TotalSentCount;
        TodaySentCount = snapshot.TodaySentCount;
        AppCodeType = snapshot.AppCodeType;
    }
}
