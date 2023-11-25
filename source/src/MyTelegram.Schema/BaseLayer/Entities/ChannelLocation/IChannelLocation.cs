// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Geographical location of supergroup (geogroups)
/// See <a href="https://corefork.telegram.org/constructor/ChannelLocation" />
///</summary>
[JsonDerivedType(typeof(TChannelLocationEmpty), nameof(TChannelLocationEmpty))]
[JsonDerivedType(typeof(TChannelLocation), nameof(TChannelLocation))]
public interface IChannelLocation : IObject
{

}
