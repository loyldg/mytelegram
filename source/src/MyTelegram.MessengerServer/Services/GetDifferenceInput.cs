namespace MyTelegram.MessengerServer.Services;

public class GetDifferenceInput
{
    public GetDifferenceInput(long ownerPeerId,
        int pts,
        int limit,
        long selfUserId)
    {
        OwnerPeerId = ownerPeerId;
        Pts = pts;
        Limit = limit;
        SelfUserId = selfUserId;
    }

    public int Limit { get; }

    public long OwnerPeerId { get; }
    public int Pts { get; }
    public long SelfUserId { get; }
}