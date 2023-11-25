// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Info about bots (available bot commands, etc)
/// See <a href="https://corefork.telegram.org/constructor/BotInfo" />
///</summary>
[JsonDerivedType(typeof(TBotInfo), nameof(TBotInfo))]
public interface IBotInfo : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// ID of the bot
    ///</summary>
    long? UserId { get; set; }

    ///<summary>
    /// Description of the bot
    ///</summary>
    string? Description { get; set; }

    ///<summary>
    /// Description photo
    /// See <a href="https://corefork.telegram.org/type/Photo" />
    ///</summary>
    MyTelegram.Schema.IPhoto? DescriptionPhoto { get; set; }

    ///<summary>
    /// Description animation in MPEG4 format
    /// See <a href="https://corefork.telegram.org/type/Document" />
    ///</summary>
    MyTelegram.Schema.IDocument? DescriptionDocument { get; set; }

    ///<summary>
    /// Bot commands that can be used in the chat
    /// See <a href="https://corefork.telegram.org/type/BotCommand" />
    ///</summary>
    TVector<MyTelegram.Schema.IBotCommand>? Commands { get; set; }

    ///<summary>
    /// Indicates the action to execute when pressing the in-UI menu button for bots
    /// See <a href="https://corefork.telegram.org/type/BotMenuButton" />
    ///</summary>
    MyTelegram.Schema.IBotMenuButton? MenuButton { get; set; }
}
