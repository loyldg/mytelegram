// ReSharper disable All

namespace MyTelegram.Schema.Stats;

///<summary>
/// Contains info about the forwards of a <a href="https://corefork.telegram.org/api/stories">story</a> as a message to public chats and reposts by public channels.
/// See <a href="https://corefork.telegram.org/constructor/stats.PublicForwards" />
///</summary>
[JsonDerivedType(typeof(TPublicForwards), nameof(TPublicForwards))]
public interface IPublicForwards : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Total number of results
    ///</summary>
    int Count { get; set; }

    ///<summary>
    /// Info about the forwards of a story.
    /// See <a href="https://corefork.telegram.org/type/PublicForward" />
    ///</summary>
    TVector<MyTelegram.Schema.IPublicForward> Forwards { get; set; }

    ///<summary>
    /// Offset used for <a href="https://corefork.telegram.org/api/offsets">pagination</a>.
    ///</summary>
    string? NextOffset { get; set; }

    ///<summary>
    /// Mentioned chats
    /// See <a href="https://corefork.telegram.org/type/Chat" />
    ///</summary>
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }

    ///<summary>
    /// Mentioned users
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
