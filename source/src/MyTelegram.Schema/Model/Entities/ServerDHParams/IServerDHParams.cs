// ReSharper disable All

namespace MyTelegram.Schema;

public interface IServerDHParams : IObject
{
    byte[] Nonce { get; set; }
    byte[] ServerNonce { get; set; }
    byte[] EncryptedAnswer { get; set; }

}
