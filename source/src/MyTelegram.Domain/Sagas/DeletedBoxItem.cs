namespace MyTelegram.Domain.Sagas;

public class DeletedBoxItem
{
    public DeletedBoxItem(long ownerPeerId,
        int pts,
        int ptsCount,
        IReadOnlyList<int> deletedMessageIdList
    )
    {
        OwnerPeerId = ownerPeerId;
        Pts = pts;
        PtsCount = ptsCount;
        DeletedMessageIdList = deletedMessageIdList;
    }

    public IReadOnlyList<int> DeletedMessageIdList { get; }

    public long OwnerPeerId { get; }
    public int Pts { get; }
    public int PtsCount { get; }
}
