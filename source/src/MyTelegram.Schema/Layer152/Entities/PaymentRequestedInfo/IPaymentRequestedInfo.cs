// ReSharper disable All

namespace MyTelegram.Schema;

public interface IPaymentRequestedInfo : IObject
{
    BitArray Flags { get; set; }
    string? Name { get; set; }
    string? Phone { get; set; }
    string? Email { get; set; }
    MyTelegram.Schema.IPostAddress? ShippingAddress { get; set; }
}
