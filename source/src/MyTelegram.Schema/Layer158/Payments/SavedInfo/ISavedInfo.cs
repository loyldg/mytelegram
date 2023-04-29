// ReSharper disable All

namespace MyTelegram.Schema.Payments;

public interface ISavedInfo : IObject
{
    BitArray Flags { get; set; }
    bool HasSavedCredentials { get; set; }
    Schema.IPaymentRequestedInfo? SavedInfo { get; set; }
}
