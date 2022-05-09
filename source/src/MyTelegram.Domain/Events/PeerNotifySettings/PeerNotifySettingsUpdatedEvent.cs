namespace MyTelegram.Domain.Events.PeerNotifySettings;

public class PeerNotifySettingsUpdatedEvent : RequestAggregateEvent<PeerNotifySettingsAggregate, PeerNotifySettingsId>
{
    public PeerNotifySettingsUpdatedEvent(long reqMsgId,
        long ownerPeerId,
        PeerType peerType,
        long peerId,
        ValueObjects.PeerNotifySettings peerNotifySettings) : base(reqMsgId)
    {
        OwnerPeerId = ownerPeerId;
        PeerType = peerType;
        PeerId = peerId;
        PeerNotifySettings = peerNotifySettings;
        //ShowPreviews = showPreviews;
        //Silent = silent;
        //MuteUntil = muteUntil;
        //Sound = sound;
    }

    public long OwnerPeerId { get; }
    public long PeerId { get; }

    public ValueObjects.PeerNotifySettings PeerNotifySettings { get; }

    public PeerType PeerType { get; }
    //public bool ShowPreviews { get; }// = true;
    //public bool Silent { get; }
    //public int MuteUntil { get; }// = int.MaxValue;
    //public string Sound { get; }// = "default";
}
