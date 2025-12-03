
namespace MyTelegram.Domain.Aggregates.AppCode;

public class AppCodeAggregate : SnapshotAggregateRoot<AppCodeAggregate, AppCodeId, AppCodeSnapshot>
{
    private readonly int _expireMinutes = 10;
    private readonly int _maxAllowedSendCountPerDay = 100;
    private readonly int _maxFailedCount = 5;
    private readonly AppCodeState _state = new();

    public AppCodeAggregate(AppCodeId id) : base(id, SnapshotEveryFewVersionsStrategy.Default)
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

    public void CheckSignInCode(RequestInfo requestInfo,
        string code,
        long userId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);

        var isCodeValid = CheckCodeCore(code,
            _maxFailedCount,
            RpcErrors.RpcErrors400.PhoneCodeInvalid,
            RpcErrors.RpcErrors400.PhoneCodeExpired);

        Emit(new CheckSignInCodeCompletedEvent(requestInfo,
            isCodeValid,
            userId));
    }

    public void CheckSignUpCode(RequestInfo requestInfo,
        long userId,
        string phoneCodeHash,
        long accessHash,
        string phoneNumber,
        string firstName,
        string? lastName)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);

        var isCodeValid = CheckCodeCore(_state.Code,
            _maxFailedCount,
            RpcErrors.RpcErrors400.PhoneCodeInvalid,
            RpcErrors.RpcErrors400.PhoneCodeExpired);

        Emit(new CheckSignUpCodeCompletedEvent(requestInfo,
            isCodeValid,
            userId,
            accessHash,
            phoneNumber,
            firstName,
            lastName
        ));
    }

    public void CreateAppCode(RequestInfo requestInfo,
        long userId,
        string phoneNumber,
        string code,
        string phoneCodeHash,
        long creationTime)
    {
        Specs.AggregateIsNew.ThrowDomainErrorIfNotSatisfied(this);
        var expire = GetExpirationMinutes();

        Emit(new AppCodeCreatedEvent(requestInfo,
            userId,
            phoneNumber,
            code,
            expire,
            phoneCodeHash,
            creationTime));
    }

    protected override Task<AppCodeSnapshot> CreateSnapshotAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(new AppCodeSnapshot(_state.UserId, _state.Expire, _state.FailedCount, _state.PhoneNumber, _state.PhoneCodeHash, _state.Code,
            _state.Email, _state.LastSmsCodeSendDate, _state.LastEmailCodeSendDate, _state.TotalSentCount,
            _state.TodaySentCount, _state.AppCodeType));
    }

    protected override Task LoadSnapshotAsync(AppCodeSnapshot snapshot, ISnapshotMetadata metadata,
        CancellationToken cancellationToken)
    {
        _state.LoadSnapshot(snapshot);

        return Task.CompletedTask;
    }

    private bool CheckCodeCore(string code, int maxFailedCount,
                RpcError? invalidCodeError = null,
        RpcError? codeExpiredError = null
    )
    {
        if (code.IsNullOrEmpty())
        {
            RpcErrors.RpcErrors400.PhoneCodeEmpty.ThrowRpcError();
        }

        if (_state.FailedCount > maxFailedCount)
        {
            (invalidCodeError ?? RpcErrors.RpcErrors400.CodeInvalid).ThrowRpcError();
        }

        var now = DateTime.UtcNow.ToTimestamp();
        if (now > _state.Expire || _state.Canceled)
        {
            (codeExpiredError ?? RpcErrors.RpcErrors400.PasswordRecoveryExpired).ThrowRpcError();
        }

        return string.Equals(_state.Code, code, StringComparison.OrdinalIgnoreCase);
    }

    private int GetExpirationMinutes()
    {
        return DateTime.UtcNow.AddMinutes(_expireMinutes).ToTimestamp();
    }
}