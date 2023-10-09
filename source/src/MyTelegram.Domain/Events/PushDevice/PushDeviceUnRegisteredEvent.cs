namespace MyTelegram.Domain.Events.PushDevice;

public class PushDeviceUnRegisteredEvent : RequestAggregateEvent2<PushDeviceAggregate, PushDeviceId>
{
    public PushDeviceUnRegisteredEvent(RequestInfo requestInfo,
        int tokenType,
        string token,
        IReadOnlyList<long> otherUids) : base(requestInfo)
    {
        TokenType = tokenType;
        Token = token;
        OtherUids = otherUids;
    }

    public IReadOnlyList<long> OtherUids { get; }
    public string Token { get; }
    public int TokenType { get; }
}
