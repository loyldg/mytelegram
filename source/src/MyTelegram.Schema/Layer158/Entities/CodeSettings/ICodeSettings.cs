// ReSharper disable All

namespace MyTelegram.Schema;

public interface ICodeSettings : IObject
{
    BitArray Flags { get; set; }
    bool AllowFlashcall { get; set; }
    bool CurrentNumber { get; set; }
    bool AllowAppHash { get; set; }
    bool AllowMissedCall { get; set; }
    bool AllowFirebase { get; set; }
    TVector<byte[]>? LogoutTokens { get; set; }
    string? Token { get; set; }
    bool? AppSandbox { get; set; }
}
