// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Geolocated peer
/// See <a href="https://corefork.telegram.org/constructor/PeerLocated" />
///</summary>
public interface IPeerLocated : IObject
{
    ///<summary>
    /// Expiry of geolocation info for current peer
    ///</summary>
    int Expires { get; set; }
}
