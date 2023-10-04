// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Public key to use <strong>only</strong> during handshakes to <a href="https://corefork.telegram.org/cdn">CDN</a> DCs.
/// See <a href="https://corefork.telegram.org/constructor/CdnPublicKey" />
///</summary>
public interface ICdnPublicKey : IObject
{
    ///<summary>
    /// <a href="https://corefork.telegram.org/cdn">CDN DC</a> ID
    ///</summary>
    int DcId { get; set; }

    ///<summary>
    /// RSA public key
    ///</summary>
    string PublicKey { get; set; }
}
