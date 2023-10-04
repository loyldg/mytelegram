// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Bot or inline keyboard buttons
/// See <a href="https://corefork.telegram.org/constructor/KeyboardButton" />
///</summary>
public interface IKeyboardButton : IObject
{
    ///<summary>
    /// Button text
    ///</summary>
    string Text { get; set; }
}
