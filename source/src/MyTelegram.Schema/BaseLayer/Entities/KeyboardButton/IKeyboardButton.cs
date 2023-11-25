// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Bot or inline keyboard buttons
/// See <a href="https://corefork.telegram.org/constructor/KeyboardButton" />
///</summary>
[JsonDerivedType(typeof(TKeyboardButton), nameof(TKeyboardButton))]
[JsonDerivedType(typeof(TKeyboardButtonUrl), nameof(TKeyboardButtonUrl))]
[JsonDerivedType(typeof(TKeyboardButtonCallback), nameof(TKeyboardButtonCallback))]
[JsonDerivedType(typeof(TKeyboardButtonRequestPhone), nameof(TKeyboardButtonRequestPhone))]
[JsonDerivedType(typeof(TKeyboardButtonRequestGeoLocation), nameof(TKeyboardButtonRequestGeoLocation))]
[JsonDerivedType(typeof(TKeyboardButtonSwitchInline), nameof(TKeyboardButtonSwitchInline))]
[JsonDerivedType(typeof(TKeyboardButtonGame), nameof(TKeyboardButtonGame))]
[JsonDerivedType(typeof(TKeyboardButtonBuy), nameof(TKeyboardButtonBuy))]
[JsonDerivedType(typeof(TKeyboardButtonUrlAuth), nameof(TKeyboardButtonUrlAuth))]
[JsonDerivedType(typeof(TInputKeyboardButtonUrlAuth), nameof(TInputKeyboardButtonUrlAuth))]
[JsonDerivedType(typeof(TKeyboardButtonRequestPoll), nameof(TKeyboardButtonRequestPoll))]
[JsonDerivedType(typeof(TInputKeyboardButtonUserProfile), nameof(TInputKeyboardButtonUserProfile))]
[JsonDerivedType(typeof(TKeyboardButtonUserProfile), nameof(TKeyboardButtonUserProfile))]
[JsonDerivedType(typeof(TKeyboardButtonWebView), nameof(TKeyboardButtonWebView))]
[JsonDerivedType(typeof(TKeyboardButtonSimpleWebView), nameof(TKeyboardButtonSimpleWebView))]
[JsonDerivedType(typeof(TKeyboardButtonRequestPeer), nameof(TKeyboardButtonRequestPeer))]
public interface IKeyboardButton : IObject
{
    ///<summary>
    /// Button text
    ///</summary>
    string Text { get; set; }
}
