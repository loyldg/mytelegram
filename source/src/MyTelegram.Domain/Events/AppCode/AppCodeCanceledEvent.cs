namespace MyTelegram.Domain.Events.AppCode;

public class AppCodeCanceledEvent : RequestAggregateEvent<AppCodeAggregate, AppCodeId>
{
    public AppCodeCanceledEvent(long reqMsgId,
        string phoneNumber,
        string phoneCodeHash) : base(reqMsgId)
    {
        PhoneNumber = phoneNumber;
        PhoneCodeHash = phoneCodeHash;
    }

    public string PhoneCodeHash { get; }

    public string PhoneNumber { get; }
}
