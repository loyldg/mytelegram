// ReSharper disable All

namespace MyTelegram.Schema;

public interface IInvoice : IObject
{
    BitArray Flags { get; set; }
    bool Test { get; set; }
    bool NameRequested { get; set; }
    bool PhoneRequested { get; set; }
    bool EmailRequested { get; set; }
    bool ShippingAddressRequested { get; set; }
    bool Flexible { get; set; }
    bool PhoneToProvider { get; set; }
    bool EmailToProvider { get; set; }
    bool Recurring { get; set; }
    string Currency { get; set; }
    TVector<MyTelegram.Schema.ILabeledPrice> Prices { get; set; }
    long? MaxTipAmount { get; set; }
    TVector<long>? SuggestedTipAmounts { get; set; }
    string? RecurringTermsUrl { get; set; }
}
