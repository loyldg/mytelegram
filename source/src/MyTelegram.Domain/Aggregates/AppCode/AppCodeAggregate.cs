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

    private bool CheckCode(string phoneCodeHash)
    {
        if (phoneCodeHash.IsNullOrEmpty())
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
        return string.Equals(_state.PhoneCodeHash, phoneCodeHash, StringComparison.OrdinalIgnoreCase);
    }

    public void CheckSignInCode(RequestInfo request,
        string phoneCodeHash,
        long userId,
        Guid correlationId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var isCodeValid = CheckCode(phoneCodeHash);

        Emit(new CheckSignInCodeCompletedEvent(request,
            isCodeValid,
            userId,
            correlationId));
    }

    public void CheckSignUpCode(RequestInfo request,
        long userId,
        string phoneCodeHash,
        long accessHash,
        string phoneNumber,
        string firstName,
        string? lastName,
        Guid correlationId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var isCodeValid = CheckCode(phoneCodeHash);

        if (userId == 0 && isCodeValid)
        {
            Emit(new SignUpRequiredEvent(request.ReqMsgId));
        }
        else
        {
            Emit(new CheckSignUpCodeCompletedEvent(request,
                isCodeValid,
                userId,
                accessHash,
                phoneNumber,
                firstName,
                lastName,
                correlationId));
        }
    }

    public void Create(RequestInfo request,
        long userId,
        string phoneNumber,
        string code,
        int expire,
        string phoneCodeHash,
        long creationTime)
    {
        Specs.AggregateIsNew.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new AppCodeCreatedEvent(request,
            userId,
            phoneNumber,
            code,
            expire,
            phoneCodeHash,
            creationTime));
    }
}
