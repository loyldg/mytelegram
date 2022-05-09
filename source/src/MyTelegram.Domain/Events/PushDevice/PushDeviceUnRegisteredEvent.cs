namespace MyTelegram.Domain.Events.PushDevice;

public class PushDeviceUnRegisteredEvent : RequestAggregateEvent<PushDeviceAggregate, PushDeviceId>
{
    public PushDeviceUnRegisteredEvent(long reqMsgId,
        int tokenType,
        string token,
        IReadOnlyList<long> otherUids) : base(reqMsgId)
    {
        TokenType = tokenType;
        Token = token;
        OtherUids = otherUids;
    }

    public IReadOnlyList<long> OtherUids { get; }
    public string Token { get; }
    public int TokenType { get; }
}
