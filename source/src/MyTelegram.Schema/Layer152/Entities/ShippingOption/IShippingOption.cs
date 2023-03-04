// ReSharper disable All

namespace MyTelegram.Schema;

public interface IShippingOption : IObject
{
    string Id { get; set; }
    string Title { get; set; }
    TVector<MyTelegram.Schema.ILabeledPrice> Prices { get; set; }
}
