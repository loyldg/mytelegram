namespace MyTelegram.Domain.Aggregates.QrCode;

public class QrCodeAggregate : AggregateRoot<QrCodeAggregate, QrCodeId>
{
    private readonly QrCodeState _state = new();

    public QrCodeAggregate(QrCodeId id) : base(id)
    {
        Register(_state);
    }

    public void AcceptLoginToken(long reqMsgId,
        long userId,
        byte[] token)
    {
        if (IsNew)
        {
            ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.AuthTokenInvalid);
        }

        var now = DateTime.UtcNow.ToTimestamp();
        if (_state.ExpireDate < now)
        {
            ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.AuthTokenExpired);
        }

        if (_state.IsAccepted)
        {
            ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.AuthTokenAlreadyAccepted);
        }

        if (_state.ExceptUidList != null && _state.ExceptUidList.Contains(userId))
        {
            ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.AuthTokenAlreadyAccepted);
        }

        Emit(new LoginTokenAcceptedEvent(reqMsgId,
            _state.TempAuthKeyId,
            _state.PermAuthKeyId,
            token,
            userId));
    }

    public void ExportLoginToken(long reqMsgId,
        long tempAuthKeyId,
        long permAuthKeyId,
        byte[] token,
        int expireDate,
        List<long> exceptUidList)
    {
        Emit(new QrCodeLoginTokenExportedEvent(reqMsgId,
            tempAuthKeyId,
            permAuthKeyId,
            token,
            expireDate,
            exceptUidList));
    }

    public void LoginWithTokenSuccess(long reqMsgId)
    {
        Specs.AggregateIsCreated.ThrowFirstDomainErrorIfNotSatisfied(this);
        if (!_state.IsAccepted || _state.IsLoginTokenUsed)
        {
            ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.AuthTokenInvalid);
        }

        Emit(new QrCodeLoginSuccessEvent(reqMsgId, _state.UserId));
    }
}
