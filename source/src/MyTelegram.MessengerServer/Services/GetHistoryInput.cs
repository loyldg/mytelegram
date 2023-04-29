namespace MyTelegram.MessengerServer.Services;

public class GetHistoryInput : GetPagedListInput
{
    public GetHistoryInput(long ownerPeerId,
        long selfUserId,
        Peer peer,
        int channelHistoryMinId)
    {
        OwnerPeerId = ownerPeerId;
        SelfUserId = selfUserId;
        Peer = peer;
        ChannelHistoryMinId = channelHistoryMinId;
    }

    public int ChannelHistoryMinId { get; }
    public long OwnerPeerId { get; }
    public Peer Peer { get; }
    public long SelfUserId { get; }
}
