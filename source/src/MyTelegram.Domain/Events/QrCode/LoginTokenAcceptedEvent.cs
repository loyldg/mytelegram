namespace MyTelegram.Domain.Events.QrCode;

public class LoginTokenAcceptedEvent : RequestAggregateEvent<QrCodeAggregate, QrCodeId>
{
    public LoginTokenAcceptedEvent(long reqMsgId,
        long qrCodeLoginRequestTempAuthKeyId,
        long qrCodeLoginRequestPermAuthKeyId,
        byte[] token,
        long userId) : base(reqMsgId)
    {
        QrCodeLoginRequestTempAuthKeyId = qrCodeLoginRequestTempAuthKeyId;
        QrCodeLoginRequestPermAuthKeyId = qrCodeLoginRequestPermAuthKeyId;
        Token = token;
        UserId = userId;
    }

    public long QrCodeLoginRequestPermAuthKeyId { get; }
    public long QrCodeLoginRequestTempAuthKeyId { get; }
    public byte[] Token { get; }
    public long UserId { get; }
}
