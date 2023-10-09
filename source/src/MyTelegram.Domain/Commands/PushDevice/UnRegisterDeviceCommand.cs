namespace MyTelegram.Domain.Commands.PushDevice;

public class UnRegisterDeviceCommand : RequestCommand2<PushDeviceAggregate, PushDeviceId, IExecutionResult>
{
    public UnRegisterDeviceCommand(PushDeviceId aggregateId,
        RequestInfo requestInfo,
        int tokenType,
        string token,
        IReadOnlyList<long> otherUids) : base(aggregateId, requestInfo)
    {
        TokenType = tokenType;
        Token = token;
        OtherUids = otherUids;
    }

    public IReadOnlyList<long> OtherUids { get; }
    public string Token { get; }
    public int TokenType { get; }
}
