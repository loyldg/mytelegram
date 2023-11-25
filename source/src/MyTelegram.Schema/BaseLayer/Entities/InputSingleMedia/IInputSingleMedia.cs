// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// A single media in an <a href="https://corefork.telegram.org/api/files#albums-grouped-media">album or grouped media</a> sent with <a href="https://corefork.telegram.org/method/messages.sendMultiMedia">messages.sendMultiMedia</a>.
/// See <a href="https://corefork.telegram.org/constructor/InputSingleMedia" />
///</summary>
[JsonDerivedType(typeof(TInputSingleMedia), nameof(TInputSingleMedia))]
public interface IInputSingleMedia : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// The media
    /// See <a href="https://corefork.telegram.org/type/InputMedia" />
    ///</summary>
    MyTelegram.Schema.IInputMedia Media { get; set; }

    ///<summary>
    /// Unique client media ID required to prevent message resending
    ///</summary>
    long RandomId { get; set; }

    ///<summary>
    /// A caption for the media
    ///</summary>
    string Message { get; set; }

    ///<summary>
    /// Message <a href="https://corefork.telegram.org/api/entities">entities</a> for styled text
    /// See <a href="https://corefork.telegram.org/type/MessageEntity" />
    ///</summary>
    TVector<MyTelegram.Schema.IMessageEntity>? Entities { get; set; }
}
