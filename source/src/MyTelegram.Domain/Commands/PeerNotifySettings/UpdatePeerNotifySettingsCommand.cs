namespace MyTelegram.Domain.Commands.PeerNotifySettings;

public class
    UpdatePeerNotifySettingsCommand : RequestCommand2<PeerNotifySettingsAggregate, PeerNotifySettingsId,
        IExecutionResult>
{
    public UpdatePeerNotifySettingsCommand(PeerNotifySettingsId aggregateId,
        RequestInfo requestInfo,
        long ownerPeerId,
        PeerType peerType,
        long peerId,
        bool? showPreviews,
        bool? silent,
        int? muteUntil,
        string? sound) : base(aggregateId, requestInfo)
    {
        OwnerPeerId = ownerPeerId;
        PeerType = peerType;
        PeerId = peerId;
        ShowPreviews = showPreviews;
        Silent = silent;
        MuteUntil = muteUntil;
        Sound = sound;
    }

    public int? MuteUntil { get; } // = int.MaxValue;
    public long OwnerPeerId { get; }
    public long PeerId { get; }
    public PeerType PeerType { get; }
    public bool? ShowPreviews { get; } // = true;
    public bool? Silent { get; }
    public string? Sound { get; } // = "default";
}