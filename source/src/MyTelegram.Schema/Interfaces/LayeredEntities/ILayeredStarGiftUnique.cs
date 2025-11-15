namespace MyTelegram.Schema;

public interface ILayeredStarGiftUnique : IStarGift
{
    IPeer? OwnerId { get; set; }
    TVector<IStarGiftAttribute> Attributes { get; set; }
    IPeer? ReleasedBy { get; set; }
    string Slug { get; set; }
    long GiftId { get; set; }
    int AvailabilityTotal { get; set; }
    int AvailabilityIssued { get; set; }
    TVector<MyTelegram.Schema.IStarsAmount>? ResellAmount { get; set; }
    MyTelegram.Schema.IPeerColor? PeerColor { get; set; }
}