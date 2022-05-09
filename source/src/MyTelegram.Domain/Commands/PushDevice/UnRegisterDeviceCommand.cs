namespace MyTelegram.Domain.Commands.PushDevice;

public class UnRegisterDeviceCommand : RequestCommand<PushDeviceAggregate, PushDeviceId, IExecutionResult>
{
    public UnRegisterDeviceCommand(PushDeviceId aggregateId,
        long reqMsgId,
        int tokenType,
        string token,
        IReadOnlyList<long> otherUids) : base(aggregateId, reqMsgId)
    {
        TokenType = tokenType;
        Token = token;
        OtherUids = otherUids;
    }

    public IReadOnlyList<long> OtherUids { get; }
    public string Token { get; }
    public int TokenType { get; }
}
