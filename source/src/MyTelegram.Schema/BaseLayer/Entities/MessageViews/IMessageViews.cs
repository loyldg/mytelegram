// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// View, forward counter + info about replies of a specific message
/// See <a href="https://corefork.telegram.org/constructor/MessageViews" />
///</summary>
[JsonDerivedType(typeof(TMessageViews), nameof(TMessageViews))]
public interface IMessageViews : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// View count of message
    ///</summary>
    int? Views { get; set; }

    ///<summary>
    /// Forward count of message
    ///</summary>
    int? Forwards { get; set; }

    ///<summary>
    /// Reply and <a href="https://corefork.telegram.org/api/threads">thread</a> information of message
    /// See <a href="https://corefork.telegram.org/type/MessageReplies" />
    ///</summary>
    MyTelegram.Schema.IMessageReplies? Replies { get; set; }
}
