using MyTelegram.Domain.Commands;

namespace MyTelegram.Domain.Aggregates.PeerSettings;

public class HidePeerSettingsBarCommand : RequestCommand2<PeerSettingsAggregate, PeerSettingsId, IExecutionResult>
{
    public long PeerId { get; }

    public HidePeerSettingsBarCommand(PeerSettingsId aggregateId, RequestInfo requestInfo, long peerId) : base(aggregateId, requestInfo)
    {
        PeerId = peerId;
    }
}