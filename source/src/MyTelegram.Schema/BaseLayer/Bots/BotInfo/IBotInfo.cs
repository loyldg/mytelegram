// ReSharper disable All

namespace MyTelegram.Schema.Bots;

///<summary>
/// Localized name, about text and description of a bot.
/// See <a href="https://corefork.telegram.org/constructor/bots.BotInfo" />
///</summary>
[JsonDerivedType(typeof(TBotInfo), nameof(TBotInfo))]
public interface IBotInfo : IObject
{
    ///<summary>
    /// Bot name
    ///</summary>
    string Name { get; set; }

    ///<summary>
    /// Bot about text
    ///</summary>
    string About { get; set; }

    ///<summary>
    /// Bot description
    ///</summary>
    string Description { get; set; }
}
