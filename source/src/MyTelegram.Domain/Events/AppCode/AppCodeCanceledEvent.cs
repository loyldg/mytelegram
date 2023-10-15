namespace MyTelegram.Domain.Events.AppCode;

public class AppCodeCanceledEvent : RequestAggregateEvent2<AppCodeAggregate, AppCodeId>
{
    public AppCodeCanceledEvent(RequestInfo requestInfo,
        string phoneNumber,
        string phoneCodeHash) : base(requestInfo)
    {
        PhoneNumber = phoneNumber;
        PhoneCodeHash = phoneCodeHash;
    }

    public string PhoneCodeHash { get; }

    public string PhoneNumber { get; }
}