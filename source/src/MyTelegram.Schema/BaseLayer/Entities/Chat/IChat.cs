// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Object defines a group.
/// See <a href="https://corefork.telegram.org/constructor/Chat" />
///</summary>
[JsonDerivedType(typeof(TChatEmpty), nameof(TChatEmpty))]
[JsonDerivedType(typeof(TChat), nameof(TChat))]
[JsonDerivedType(typeof(TChatForbidden), nameof(TChatForbidden))]
[JsonDerivedType(typeof(TChannel), nameof(TChannel))]
[JsonDerivedType(typeof(TChannelForbidden), nameof(TChannelForbidden))]
public interface IChat : IObject
{
    ///<summary>
    /// Channel ID
    ///</summary>
    long Id { get; set; }
}
