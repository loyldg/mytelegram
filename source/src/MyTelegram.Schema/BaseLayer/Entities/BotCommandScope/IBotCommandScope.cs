// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Represents a scope where the bot commands, specified using <a href="https://corefork.telegram.org/method/bots.setBotCommands">bots.setBotCommands</a> will be valid.
/// See <a href="https://corefork.telegram.org/constructor/BotCommandScope" />
///</summary>
[JsonDerivedType(typeof(TBotCommandScopeDefault), nameof(TBotCommandScopeDefault))]
[JsonDerivedType(typeof(TBotCommandScopeUsers), nameof(TBotCommandScopeUsers))]
[JsonDerivedType(typeof(TBotCommandScopeChats), nameof(TBotCommandScopeChats))]
[JsonDerivedType(typeof(TBotCommandScopeChatAdmins), nameof(TBotCommandScopeChatAdmins))]
[JsonDerivedType(typeof(TBotCommandScopePeer), nameof(TBotCommandScopePeer))]
[JsonDerivedType(typeof(TBotCommandScopePeerAdmins), nameof(TBotCommandScopePeerAdmins))]
[JsonDerivedType(typeof(TBotCommandScopePeerUser), nameof(TBotCommandScopePeerUser))]
public interface IBotCommandScope : IObject
{

}
