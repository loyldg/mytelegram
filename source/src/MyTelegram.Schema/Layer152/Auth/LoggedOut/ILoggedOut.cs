// ReSharper disable All

namespace MyTelegram.Schema.Auth;

public interface ILoggedOut : IObject
{
    BitArray Flags { get; set; }
    byte[]? FutureAuthToken { get; set; }
}
