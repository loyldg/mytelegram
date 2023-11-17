// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/PrepaidGiveaway" />
///</summary>
public interface IPrepaidGiveaway : IObject
{
    long Id { get; set; }
    int Months { get; set; }
    int Quantity { get; set; }
    int Date { get; set; }
}
