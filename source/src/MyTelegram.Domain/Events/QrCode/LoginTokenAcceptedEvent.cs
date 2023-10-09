namespace MyTelegram.Domain.Events.QrCode;

public class LoginTokenAcceptedEvent : RequestAggregateEvent2<QrCodeAggregate, QrCodeId>
{
    public LoginTokenAcceptedEvent(RequestInfo requestInfo,
        long qrCodeLoginRequestTempAuthKeyId,
        long qrCodeLoginRequestPermAuthKeyId,
        byte[] token,
        long userId) : base(requestInfo)
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