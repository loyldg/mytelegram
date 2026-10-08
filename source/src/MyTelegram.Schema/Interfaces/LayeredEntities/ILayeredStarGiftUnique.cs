namespace MyTelegram.Schema;

public partial interface ILayeredStarGiftUnique : IStarGift
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
    int? OfferMinStars { get; set; }
    int? CraftChancePermille { get; set; }
    bool ResaleTonOnly { get; set; }
    /// <summary>
    /// Price of the gift.
    /// </summary>
    long? ValueAmount { get; set; }

    /// <summary>
    /// Currency for the gift's price.
    /// </summary>
    string? ValueCurrency { get; set; }

    /// <summary>
    ///  
    /// </summary>
    long? ValueUsdAmount { get; set; }
}