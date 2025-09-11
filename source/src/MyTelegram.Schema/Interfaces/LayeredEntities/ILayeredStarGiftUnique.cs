namespace MyTelegram.Schema;

public interface ILayeredStarGiftUnique : IStarGift
{
    IPeer? OwnerId { get; set; }
    TVector<IStarGiftAttribute> Attributes { get; set; }
    IPeer? ReleasedBy { get; set; }
}