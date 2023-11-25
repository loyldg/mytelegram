// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Configuration for <a href="https://corefork.telegram.org/cdn">CDN</a> file downloads.
/// See <a href="https://corefork.telegram.org/constructor/CdnConfig" />
///</summary>
[JsonDerivedType(typeof(TCdnConfig), nameof(TCdnConfig))]
public interface ICdnConfig : IObject
{
    ///<summary>
    /// Vector of public keys to use <strong>only</strong> during handshakes to <a href="https://corefork.telegram.org/cdn">CDN</a> DCs.
    /// See <a href="https://corefork.telegram.org/type/CdnPublicKey" />
    ///</summary>
    TVector<MyTelegram.Schema.ICdnPublicKey> PublicKeys { get; set; }
}
