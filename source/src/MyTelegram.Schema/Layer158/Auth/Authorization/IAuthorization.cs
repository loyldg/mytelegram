// ReSharper disable All

namespace MyTelegram.Schema.Auth;

public interface IAuthorization : IObject
{
    BitArray Flags { get; set; }
}
