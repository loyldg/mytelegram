// ReSharper disable All

namespace MyTelegram.Schema.Account;

public interface IPasswordInputSettings : IObject
{
    BitArray Flags { get; set; }
    MyTelegram.Schema.IPasswordKdfAlgo? NewAlgo { get; set; }
    byte[]? NewPasswordHash { get; set; }
    string? Hint { get; set; }
    string? Email { get; set; }
    MyTelegram.Schema.ISecureSecretSettings? NewSecureSettings { get; set; }
}
