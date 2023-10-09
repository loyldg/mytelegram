namespace MyTelegram.Domain.Events.QrCode;

public class QrCodeLoginSuccessEvent : RequestAggregateEvent2<QrCodeAggregate, QrCodeId>
{
    public QrCodeLoginSuccessEvent(RequestInfo requestInfo,
        long userId) : base(requestInfo)
    {
        UserId = userId;
    }

    public long UserId { get; }
}