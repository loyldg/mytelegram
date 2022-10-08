// ReSharper disable All

namespace MyTelegram.Schema;

public interface IPremiumSubscriptionOption : IObject
{
    BitArray Flags { get; set; }
    bool Current { get; set; }
    bool CanPurchaseUpgrade { get; set; }
    int Months { get; set; }
    string Currency { get; set; }
    long Amount { get; set; }
    string BotUrl { get; set; }
    string? StoreProduct { get; set; }

}
