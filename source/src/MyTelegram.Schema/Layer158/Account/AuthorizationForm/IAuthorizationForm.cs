// ReSharper disable All

namespace MyTelegram.Schema.Account;

public interface IAuthorizationForm : IObject
{
    BitArray Flags { get; set; }
    TVector<Schema.ISecureRequiredType> RequiredTypes { get; set; }
    TVector<Schema.ISecureValue> Values { get; set; }
    TVector<Schema.ISecureValueError> Errors { get; set; }
    TVector<Schema.IUser> Users { get; set; }
    string? PrivacyPolicyUrl { get; set; }
}
