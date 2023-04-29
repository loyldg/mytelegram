namespace MyTelegram.MessengerServer.Services;

public class ReorderPinnedDialogsInput
{
    public ReorderPinnedDialogsInput(long selfUserId,
        List<Peer> orderedPeerList)
    {
        SelfUserId = selfUserId;
        OrderedPeerList = orderedPeerList;
    }

    public List<Peer> OrderedPeerList { get; }

    public long SelfUserId { get; }
}
