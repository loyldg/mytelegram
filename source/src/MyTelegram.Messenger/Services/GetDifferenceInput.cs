namespace MyTelegram.Messenger.Services;

public class GetDifferenceInput
{
    public GetDifferenceInput(
        long selfUserId,
        long ownerPeerId,
        int pts,
        int limit,
        List<int>? messageIds,
        List<long>? users = null,
       List<long>? chats = null)
    {
        OwnerPeerId = ownerPeerId;
        Pts = pts;
        Limit = limit;
        MessageIds = messageIds;
        Users = users;
        Chats = chats;
        SelfUserId = selfUserId;
    }

    public int Limit { get; }
    public List<int>? MessageIds { get; }
    public List<long>? Users { get; }
    public List<long>? Chats { get; }

    public long OwnerPeerId { get; }
    public int Pts { get; }
    public long SelfUserId { get; }
}