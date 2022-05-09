namespace MyTelegram.Domain.Events.Device;

public class DeviceRegisteredEvent : RequestAggregateEvent<DeviceAggregate, DeviceId>
{
    public DeviceRegisteredEvent(long reqMsgId,
        int tokenType,
        string token) : base(reqMsgId)
    {
        TokenType = tokenType;
        Token = token;
    }

    public string Token { get; }
    public int TokenType { get; }
}
