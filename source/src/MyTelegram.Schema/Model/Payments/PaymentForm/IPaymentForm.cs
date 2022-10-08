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
    MyTelegram.Schema.IWebDocument? Photo { get; set; }
    MyTelegram.Schema.IInvoice Invoice { get; set; }
    long ProviderId { get; set; }
    string Url { get; set; }
    string? NativeProvider { get; set; }
    MyTelegram.Schema.IDataJSON? NativeParams { get; set; }
    TVector<MyTelegram.Schema.IPaymentFormMethod>? AdditionalMethods { get; set; }
    MyTelegram.Schema.IPaymentRequestedInfo? SavedInfo { get; set; }
    TVector<MyTelegram.Schema.IPaymentSavedCredentials>? SavedCredentials { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }

}
