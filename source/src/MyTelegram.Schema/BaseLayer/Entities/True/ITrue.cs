// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/mtproto/TL-formal#predefined-identifiers">predefined identifiers</a>.
/// See <a href="https://corefork.telegram.org/constructor/True" />
///</summary>
[JsonDerivedType(typeof(TTrue), nameof(TTrue))]
public interface ITrue : IObject
{

}
