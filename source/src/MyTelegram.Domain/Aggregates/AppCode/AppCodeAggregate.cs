namespace MyTelegram.Domain.Aggregates.AppCode;

public class AppCodeAggregate : AggregateRoot<AppCodeAggregate, AppCodeId>
{
    private readonly int _maxFailedCount = 5;
    private readonly AppCodeState _state = new();

    public AppCodeAggregate(AppCodeId id) : base(id)
    {
        Register(_state);
    }

    public void CancelCode(long reqMsgId,
        string phoneNumber,
        string phoneCodeHash)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new AppCodeCanceledEvent(reqMsgId, phoneNumber, phoneCodeHash));
    }

    private bool CheckCode(string code)
    {
        if (code.IsNullOrEmpty())
        {
            ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.PhoneCodeEmpty);
        }

        if (_state.FailedCount > _maxFailedCount)
        {
            ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.PhoneCodeInvalid);
        }

        var now = DateTime.UtcNow.ToTimestamp();
        if (now > _state.Expire || _state.Canceled)
        {
            ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.PhoneCodeExpired);
        }

        // the validation failed count should be saved,so not throw exception
        return string.Equals(_state.Code, code, StringComparison.OrdinalIgnoreCase);
    }

    public void CheckSignInCode(RequestInfo requestInfo,
        //string phoneCodeHash,
        string code,
        long userId,
        Guid correlationId)
    {
        var isCodeValid = CheckCode(code);

        Emit(new CheckSignInCodeCompletedEvent(requestInfo,
            isCodeValid,
            userId,
            correlationId));
    }

    public void CheckSignUpCode(RequestInfo requestInfo,
        long userId,
        string phoneCodeHash,
        long accessHash,
        string phoneNumber,
        string firstName,
        string? lastName,
        Guid correlationId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        //var isCodeValid = CheckCode(code);

        Emit(new CheckSignUpCodeCompletedEvent(requestInfo,
            true,
            userId,
            accessHash,
            phoneNumber,
            firstName,
            lastName,
            correlationId));
    }

    public void Create(RequestInfo requestInfo,
        long userId,
        string phoneNumber,
        string code,
        int expire,
        string phoneCodeHash,
        long creationTime)
    {
        Specs.AggregateIsNew.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new AppCodeCreatedEvent(requestInfo,
            userId,
            phoneNumber,
            code,
            expire,
            phoneCodeHash,
            creationTime));
    }
}
