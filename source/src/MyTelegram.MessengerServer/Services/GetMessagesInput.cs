namespace MyTelegram.MessengerServer.Services;

public class GetMessagesInput : GetPagedListInput
{
    public GetMessagesInput(
        long selfUserId,
        long ownerPeerId,
        List<int> messageIdList,
        Peer? peer)
    {
        SelfUserId = selfUserId;
        OwnerPeerId = ownerPeerId;
        MessageIdList = messageIdList;
        Peer = peer;
    }

    public List<int> MessageIdList { get; }
    public long OwnerPeerId { get; }
    public Peer? Peer { get; }
    public long SelfUserId { get; }
}
