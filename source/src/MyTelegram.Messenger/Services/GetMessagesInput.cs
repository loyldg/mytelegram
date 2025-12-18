namespace MyTelegram.Messenger.Services;

public class GetMessagesInput(
    long selfUserId,
    long ownerPeerId,
    List<int> messageIdList,
    Peer? peer)
    : GetPagedListInput
{
    public List<int> MessageIdList { get; } = messageIdList;
    public long OwnerPeerId { get; } = ownerPeerId;
    public Peer? Peer { get; } = peer;
    public long SelfUserId { get; } = selfUserId;
}