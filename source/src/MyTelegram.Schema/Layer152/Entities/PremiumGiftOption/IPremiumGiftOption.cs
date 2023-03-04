// ReSharper disable All

namespace MyTelegram.Schema;

public interface IPremiumGiftOption : IObject
{
    BitArray Flags { get; set; }
    int Months { get; set; }
    string Currency { get; set; }
    long Amount { get; set; }
    string BotUrl { get; set; }
    string? StoreProduct { get; set; }
}
