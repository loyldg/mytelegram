// ReSharper disable All

namespace MyTelegram.Schema.Account;

public interface IPasswordSettings : IObject
{
    BitArray Flags { get; set; }
    string? Email { get; set; }
    MyTelegram.Schema.ISecureSecretSettings? SecureSettings { get; set; }

}
