// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Used to fetch information about a <a href="https://corefork.telegram.org/api/bots/webapps#named-bot-web-apps">named bot web app</a>
/// See <a href="https://corefork.telegram.org/constructor/InputBotApp" />
///</summary>
[JsonDerivedType(typeof(TInputBotAppID), nameof(TInputBotAppID))]
[JsonDerivedType(typeof(TInputBotAppShortName), nameof(TInputBotAppShortName))]
public interface IInputBotApp : IObject
{

}
