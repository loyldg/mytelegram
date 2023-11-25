// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Geolocated peer
/// See <a href="https://corefork.telegram.org/constructor/PeerLocated" />
///</summary>
[JsonDerivedType(typeof(TPeerLocated), nameof(TPeerLocated))]
[JsonDerivedType(typeof(TPeerSelfLocated), nameof(TPeerSelfLocated))]
public interface IPeerLocated : IObject
{
    ///<summary>
    /// Expiry of geolocation info for current peer
    ///</summary>
    int Expires { get; set; }
}
