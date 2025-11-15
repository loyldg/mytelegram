namespace MyTelegram.Schema;

public interface ILayeredStarGift : IStarGift
{
    MyTelegram.Schema.IDocument Sticker { get; set; }
    bool Limited { get; set; }
    ///<summary>
    /// For limited-supply gifts: the remaining number of gifts that may be bought.
    ///</summary>
    int? AvailabilityRemains { get; set; }

    ///<summary>
    /// For limited-supply gifts: the total number of gifts that was available in the initial supply.
    ///</summary>
    int? AvailabilityTotal { get; set; }

    long? AvailabilityResale { get; set; }
}