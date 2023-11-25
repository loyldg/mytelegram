// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Message edit data for media
/// See <a href="https://corefork.telegram.org/constructor/messages.MessageEditData" />
///</summary>
[JsonDerivedType(typeof(TMessageEditData), nameof(TMessageEditData))]
public interface IMessageEditData : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Media caption, if the specified media's caption can be edited
    ///</summary>
    bool Caption { get; set; }
}
