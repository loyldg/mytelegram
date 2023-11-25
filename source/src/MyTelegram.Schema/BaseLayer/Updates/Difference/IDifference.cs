// ReSharper disable All

namespace MyTelegram.Schema.Updates;

///<summary>
/// Occurred changes.
/// See <a href="https://corefork.telegram.org/constructor/updates.Difference" />
///</summary>
[JsonDerivedType(typeof(TDifferenceEmpty), nameof(TDifferenceEmpty))]
[JsonDerivedType(typeof(TDifference), nameof(TDifference))]
[JsonDerivedType(typeof(TDifferenceSlice), nameof(TDifferenceSlice))]
[JsonDerivedType(typeof(TDifferenceTooLong), nameof(TDifferenceTooLong))]
public interface IDifference : IObject
{

}
