using MyTelegram.Domain.Commands;

namespace MyTelegram.Domain.Aggregates.PeerSettings;

public class HidePeerSettingsBarCommand : RequestCommand2<PeerSettingsAggregate, PeerSettingsId, IExecutionResult>
{
    public long PeerId { get; }

    public HidePeerSettingsBarCommand(PeerSettingsId aggregateId, RequestInfo requestInfo, long peerId) : base(aggregateId, requestInfo)
    {
        PeerId = peerId;
    }

    protected override IEnumerable<byte[]> GetSourceIdComponents()
    {
        yield return BitConverter.GetBytes(RequestInfo.ReqMsgId);
        yield return RequestInfo.RequestId.ToByteArray();
    }
}