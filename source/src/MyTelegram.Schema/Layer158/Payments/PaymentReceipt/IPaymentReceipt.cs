// ReSharper disable All

namespace MyTelegram.Schema.Payments;

public interface IPaymentReceipt : IObject
{
    BitArray Flags { get; set; }
    int Date { get; set; }
    long BotId { get; set; }
    long ProviderId { get; set; }
    string Title { get; set; }
    string Description { get; set; }
    MyTelegram.Schema.IWebDocument? Photo { get; set; }
    MyTelegram.Schema.IInvoice Invoice { get; set; }
    MyTelegram.Schema.IPaymentRequestedInfo? Info { get; set; }
    MyTelegram.Schema.IShippingOption? Shipping { get; set; }
    long? TipAmount { get; set; }
    string Currency { get; set; }
    long TotalAmount { get; set; }
    string CredentialsTitle { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
