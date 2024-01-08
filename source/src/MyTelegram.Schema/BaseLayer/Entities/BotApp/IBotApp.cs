// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Contains information about a <a href="https://corefork.telegram.org/api/bots/webapps#named-mini-apps">named Mini App</a>.
/// See <a href="https://corefork.telegram.org/constructor/BotApp" />
///</summary>
[JsonDerivedType(typeof(TBotAppNotModified), nameof(TBotAppNotModified))]
[JsonDerivedType(typeof(TBotApp), nameof(TBotApp))]
public interface IBotApp : IObject
{

}
