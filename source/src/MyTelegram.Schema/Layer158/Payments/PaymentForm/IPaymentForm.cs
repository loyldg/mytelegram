// ReSharper disable All

namespace MyTelegram.Schema.Payments;

public interface IPaymentForm : IObject
{
    BitArray Flags { get; set; }
    bool CanSaveCredentials { get; set; }
    bool PasswordMissing { get; set; }
    long FormId { get; set; }
    long BotId { get; set; }
    string Title { get; set; }
    string Description { get; set; }
    Schema.IWebDocument? Photo { get; set; }
    Schema.IInvoice Invoice { get; set; }
    long ProviderId { get; set; }
    string Url { get; set; }
    string? NativeProvider { get; set; }
    Schema.IDataJSON? NativeParams { get; set; }
    TVector<Schema.IPaymentFormMethod>? AdditionalMethods { get; set; }
    Schema.IPaymentRequestedInfo? SavedInfo { get; set; }
    TVector<Schema.IPaymentSavedCredentials>? SavedCredentials { get; set; }
    TVector<Schema.IUser> Users { get; set; }
}
