// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Boolean type.
/// See <a href="https://corefork.telegram.org/constructor/Bool" />
///</summary>
[JsonDerivedType(typeof(TBoolFalse), nameof(TBoolFalse))]
[JsonDerivedType(typeof(TBoolTrue), nameof(TBoolTrue))]
public interface IBool : IObject
{

}
