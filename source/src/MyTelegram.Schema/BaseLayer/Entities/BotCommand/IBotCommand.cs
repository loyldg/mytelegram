// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Describes a bot command that can be used in a chat
/// See <a href="https://corefork.telegram.org/constructor/BotCommand" />
///</summary>
[JsonDerivedType(typeof(TBotCommand), nameof(TBotCommand))]
public interface IBotCommand : IObject
{
    ///<summary>
    /// <code>/command</code> name
    ///</summary>
    string Command { get; set; }

    ///<summary>
    /// Description of the command
    ///</summary>
    string Description { get; set; }
}
