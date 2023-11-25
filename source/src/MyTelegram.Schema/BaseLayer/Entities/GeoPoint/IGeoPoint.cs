// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Object defines a GeoPoint.
/// See <a href="https://corefork.telegram.org/constructor/GeoPoint" />
///</summary>
[JsonDerivedType(typeof(TGeoPointEmpty), nameof(TGeoPointEmpty))]
[JsonDerivedType(typeof(TGeoPoint), nameof(TGeoPoint))]
public interface IGeoPoint : IObject
{

}
