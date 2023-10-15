namespace MyTelegram.Domain.Aggregates.AppCode;

public class AppCodeAggregate : AggregateRoot<AppCodeAggregate, AppCodeId>
{
    private readonly int _maxFailedCount = 5;
    private readonly AppCodeState _state = new();

    public AppCodeAggregate(AppCodeId id) : base(id)
    {
        Register(_state);
    }

    public void CancelCode(RequestInfo requestInfo,
        string phoneNumber,
        string phoneCodeHash)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new AppCodeCanceledEvent(requestInfo, phoneNumber, phoneCodeHash));
    }

    private bool CheckCode(string code)
    {
        if (code.IsNullOrEmpty())
        {
            //ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.PhoneCodeEmpty);
            RpcErrors.RpcErrors400.PhoneCodeEmpty.ThrowRpcError();
        }

        if (_state.FailedCount > _maxFailedCount)
        {
            //ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.PhoneCodeInvalid);
            RpcErrors.RpcErrors400.PhoneCodeInvalid.ThrowRpcError();
        }

        var now = DateTime.UtcNow.ToTimestamp();
        if (now > _state.Expire || _state.Canceled)
        {
            //ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.PhoneCodeExpired);
            RpcErrors.RpcErrors400.PhoneCodeExpired.ThrowRpcError();
        }

        // the validation failed count should be saved,so not throw exception
        return string.Equals(_state.Code, code, StringComparison.OrdinalIgnoreCase);
    }

    public void CheckSignInCode(RequestInfo requestInfo,
        //string phoneCodeHash,
        string code,
        long userId)
    {
        var isCodeValid = CheckCode(code);

        Emit(new CheckSignInCodeCompletedEvent(requestInfo,
            isCodeValid,
            userId));
    }

    public void CheckSignUpCode(RequestInfo requestInfo,
        long userId,
        string phoneCodeHash,
        //string code,
        long accessHash,
        string phoneNumber,
        string firstName,
        string? lastName)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        //var isCodeValid = CheckCode(code);
        // Sign up only check phoneCodeHash,AppCodeAggregate.AggregateIsCreated means phoneCodeHash is right,because AppCodeAggregate is created with the phoneCodeHash

        Emit(new CheckSignUpCodeCompletedEvent(requestInfo,
            true,
            userId,
            accessHash,
            phoneNumber,
            firstName,
            lastName
            ));
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
