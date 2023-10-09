namespace MyTelegram.Domain.Aggregates.QrCode;

public class QrCodeAggregate : AggregateRoot<QrCodeAggregate, QrCodeId>
{
    private readonly QrCodeState _state = new();

    public QrCodeAggregate(QrCodeId id) : base(id)
    {
        Register(_state);
    }

    public void AcceptLoginToken(RequestInfo requestInfo,
        long userId,
        byte[] token)
    {
        if (IsNew)
        {
            RpcErrors.RpcErrors400.AuthTokenInvalid.ThrowRpcError();
        }

        var now = DateTime.UtcNow.ToTimestamp();
        if (_state.ExpireDate < now)
        {
            RpcErrors.RpcErrors400.AuthTokenExpired.ThrowRpcError();
        }

        if (_state.IsAccepted)
        {
            //ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.AuthTokenAlreadyAccepted);
            RpcErrors.RpcErrors400.AuthTokenAlreadyAccepted.ThrowRpcError();
        }

        if (_state.ExceptUidList != null && _state.ExceptUidList.Contains(userId))
        {
            RpcErrors.RpcErrors400.AuthTokenAlreadyAccepted.ThrowRpcError();
        }

        Emit(new LoginTokenAcceptedEvent(requestInfo,
            _state.TempAuthKeyId,
            _state.PermAuthKeyId,
            token,
            userId));
    }

    public void ExportLoginToken(RequestInfo requestInfo,
        long tempAuthKeyId,
        long permAuthKeyId,
        byte[] token,
        int expireDate,
        List<long> exceptUidList)
    {
        Emit(new QrCodeLoginTokenExportedEvent(requestInfo,
            tempAuthKeyId,
            permAuthKeyId,
            token,
            expireDate,
            exceptUidList));
    }

    public void LoginWithTokenSuccess(RequestInfo requestInfo)
    {
        Specs.AggregateIsCreated.ThrowFirstDomainErrorIfNotSatisfied(this);
        if (!_state.IsAccepted || _state.IsLoginTokenUsed)
        {
            //ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.AuthTokenInvalid);
            RpcErrors.RpcErrors400.AuthTokenInvalid.ThrowRpcError();
        }

        Emit(new QrCodeLoginSuccessEvent(requestInfo, _state.UserId));
    }
}
