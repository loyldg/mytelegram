namespace MyTelegram.Domain.Events.QrCode;

public class QrCodeLoginSuccessEvent : RequestAggregateEvent<QrCodeAggregate, QrCodeId>
{
    public QrCodeLoginSuccessEvent(long reqMsgId,
        long userId) : base(reqMsgId)
    {
        UserId = userId;
    }

    public long UserId { get; }
}
