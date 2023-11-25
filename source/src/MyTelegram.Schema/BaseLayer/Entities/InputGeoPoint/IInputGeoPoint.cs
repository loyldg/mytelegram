// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Defines a GeoPoint.
/// See <a href="https://corefork.telegram.org/constructor/InputGeoPoint" />
///</summary>
[JsonDerivedType(typeof(TInputGeoPointEmpty), nameof(TInputGeoPointEmpty))]
[JsonDerivedType(typeof(TInputGeoPoint), nameof(TInputGeoPoint))]
public interface IInputGeoPoint : IObject
{

}
