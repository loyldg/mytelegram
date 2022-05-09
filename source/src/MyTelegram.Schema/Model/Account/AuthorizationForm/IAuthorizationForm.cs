// ReSharper disable All

namespace MyTelegram.Schema.Account;

public interface IAuthorizationForm : IObject
{
    BitArray Flags { get; set; }
    TVector<MyTelegram.Schema.ISecureRequiredType> RequiredTypes { get; set; }
    TVector<MyTelegram.Schema.ISecureValue> Values { get; set; }
    TVector<MyTelegram.Schema.ISecureValueError> Errors { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
    string? PrivacyPolicyUrl { get; set; }

}
