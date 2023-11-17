// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/PremiumGiftCodeOption" />
///</summary>
public interface IPremiumGiftCodeOption : IObject
{
    BitArray Flags { get; set; }
    int Users { get; set; }
    int Months { get; set; }
    string? StoreProduct { get; set; }
    int? StoreQuantity { get; set; }
    string Currency { get; set; }
    long Amount { get; set; }
}
