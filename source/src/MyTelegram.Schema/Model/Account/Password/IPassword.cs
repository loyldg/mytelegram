// ReSharper disable All

namespace MyTelegram.Schema.Account;

public interface IPassword : IObject
{
    BitArray Flags { get; set; }
    bool HasRecovery { get; set; }
    bool HasSecureValues { get; set; }
    bool HasPassword { get; set; }
    MyTelegram.Schema.IPasswordKdfAlgo? CurrentAlgo { get; set; }
    byte[]? SrpB { get; set; }
    long? SrpId { get; set; }
    string? Hint { get; set; }
    string? EmailUnconfirmedPattern { get; set; }
    MyTelegram.Schema.IPasswordKdfAlgo NewAlgo { get; set; }
    MyTelegram.Schema.ISecurePasswordKdfAlgo NewSecureAlgo { get; set; }
    byte[] SecureRandom { get; set; }
    int? PendingResetDate { get; set; }

}
