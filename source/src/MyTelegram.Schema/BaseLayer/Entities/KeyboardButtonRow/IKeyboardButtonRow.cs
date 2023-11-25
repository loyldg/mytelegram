// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Bot or inline keyboard rows
/// See <a href="https://corefork.telegram.org/constructor/KeyboardButtonRow" />
///</summary>
[JsonDerivedType(typeof(TKeyboardButtonRow), nameof(TKeyboardButtonRow))]
public interface IKeyboardButtonRow : IObject
{
    ///<summary>
    /// Bot or inline keyboard buttons
    /// See <a href="https://corefork.telegram.org/type/KeyboardButton" />
    ///</summary>
    TVector<MyTelegram.Schema.IKeyboardButton> Buttons { get; set; }
}
