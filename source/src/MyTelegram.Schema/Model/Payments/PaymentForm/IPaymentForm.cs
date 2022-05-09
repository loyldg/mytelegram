// ReSharper disable All

namespace MyTelegram.Schema.Payments;

public interface IPaymentForm : IObject
{
    BitArray Flags { get; set; }
    bool CanSaveCredentials { get; set; }
    bool PasswordMissing { get; set; }
    long FormId { get; set; }
    long BotId { get; set; }
    MyTelegram.Schema.IInvoice Invoice { get; set; }
    long ProviderId { get; set; }
    string Url { get; set; }
    string? NativeProvider { get; set; }
    MyTelegram.Schema.IDataJSON? NativeParams { get; set; }
    MyTelegram.Schema.IPaymentRequestedInfo? SavedInfo { get; set; }
    MyTelegram.Schema.IPaymentSavedCredentials? SavedCredentials { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }

}
