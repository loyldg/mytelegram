// ReSharper disable All

namespace MyTelegram.Schema.Account;

public interface IContentSettings : IObject
{
    BitArray Flags { get; set; }
    bool SensitiveEnabled { get; set; }
    bool SensitiveCanChange { get; set; }
}
