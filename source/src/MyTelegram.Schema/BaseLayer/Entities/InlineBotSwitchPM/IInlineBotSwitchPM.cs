// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// The bot requested the user to message them in private
/// See <a href="https://corefork.telegram.org/constructor/InlineBotSwitchPM" />
///</summary>
[JsonDerivedType(typeof(TInlineBotSwitchPM), nameof(TInlineBotSwitchPM))]
public interface IInlineBotSwitchPM : IObject
{
    ///<summary>
    /// Text for the button that switches the user to a private chat with the bot and sends the bot a start message with the parameter <code>start_parameter</code> (can be empty)
    ///</summary>
    string Text { get; set; }

    ///<summary>
    /// The parameter for the <code>/start parameter</code>
    ///</summary>
    string StartParam { get; set; }
}
