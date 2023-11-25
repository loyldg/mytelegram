// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Indicates the action to execute when pressing the in-UI menu button for bots
/// See <a href="https://corefork.telegram.org/constructor/BotMenuButton" />
///</summary>
[JsonDerivedType(typeof(TBotMenuButtonDefault), nameof(TBotMenuButtonDefault))]
[JsonDerivedType(typeof(TBotMenuButtonCommands), nameof(TBotMenuButtonCommands))]
[JsonDerivedType(typeof(TBotMenuButton), nameof(TBotMenuButton))]
public interface IBotMenuButton : IObject
{

}
