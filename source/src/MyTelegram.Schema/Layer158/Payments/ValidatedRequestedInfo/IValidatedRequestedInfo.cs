// ReSharper disable All

namespace MyTelegram.Schema.Payments;

public interface IValidatedRequestedInfo : IObject
{
    BitArray Flags { get; set; }
    string? Id { get; set; }
    TVector<Schema.IShippingOption>? ShippingOptions { get; set; }
}
