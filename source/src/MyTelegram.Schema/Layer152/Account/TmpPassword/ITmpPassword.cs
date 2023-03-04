// ReSharper disable All

namespace MyTelegram.Schema.Account;

public interface ITmpPassword : IObject
{
    byte[] TmpPassword { get; set; }
    int ValidUntil { get; set; }
}
