// ReSharper disable All

namespace MyTelegram.Schema.Account;

///<summary>
/// Sensitive content settings
/// See <a href="https://corefork.telegram.org/constructor/account.ContentSettings" />
///</summary>
public interface IContentSettings : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Whether viewing of sensitive (NSFW) content is enabled
    ///</summary>
    bool SensitiveEnabled { get; set; }

    ///<summary>
    /// Whether the current client can change the sensitive content settings to view NSFW content
    ///</summary>
    bool SensitiveCanChange { get; set; }
}
