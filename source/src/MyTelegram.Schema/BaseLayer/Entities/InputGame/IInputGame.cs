// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// A game to send
/// See <a href="https://corefork.telegram.org/constructor/InputGame" />
///</summary>
[JsonDerivedType(typeof(TInputGameID), nameof(TInputGameID))]
[JsonDerivedType(typeof(TInputGameShortName), nameof(TInputGameShortName))]
public interface IInputGame : IObject
{

}
