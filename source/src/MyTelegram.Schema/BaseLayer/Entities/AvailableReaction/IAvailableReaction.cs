// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Animations associated with a message reaction
/// See <a href="https://corefork.telegram.org/constructor/AvailableReaction" />
///</summary>
public interface IAvailableReaction : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// If not set, the reaction can be added to new messages and enabled in chats.
    ///</summary>
    bool Inactive { get; set; }

    ///<summary>
    /// Whether this reaction can only be used by Telegram Premium users
    ///</summary>
    bool Premium { get; set; }

    ///<summary>
    /// Reaction emoji
    ///</summary>
    string Reaction { get; set; }

    ///<summary>
    /// Reaction description
    ///</summary>
    string Title { get; set; }

    ///<summary>
    /// Static icon for the reaction
    /// See <a href="https://corefork.telegram.org/type/Document" />
    ///</summary>
    MyTelegram.Schema.IDocument StaticIcon { get; set; }

    ///<summary>
    /// The animated sticker to show when the user opens the reaction dropdown
    /// See <a href="https://corefork.telegram.org/type/Document" />
    ///</summary>
    MyTelegram.Schema.IDocument AppearAnimation { get; set; }

    ///<summary>
    /// The animated sticker to show when the user hovers over the reaction
    /// See <a href="https://corefork.telegram.org/type/Document" />
    ///</summary>
    MyTelegram.Schema.IDocument SelectAnimation { get; set; }

    ///<summary>
    /// The animated sticker to show when the reaction is chosen and activated
    /// See <a href="https://corefork.telegram.org/type/Document" />
    ///</summary>
    MyTelegram.Schema.IDocument ActivateAnimation { get; set; }

    ///<summary>
    /// The background effect (still an animated sticker) to play under the <code>activate_animation</code>, when the reaction is chosen and activated
    /// See <a href="https://corefork.telegram.org/type/Document" />
    ///</summary>
    MyTelegram.Schema.IDocument EffectAnimation { get; set; }

    ///<summary>
    /// The animation that plays around the button when you press an existing reaction (played together with <code>center_icon</code>).
    /// See <a href="https://corefork.telegram.org/type/Document" />
    ///</summary>
    MyTelegram.Schema.IDocument? AroundAnimation { get; set; }

    ///<summary>
    /// The animation of the emoji inside the button when you press an existing reaction (played together with <code>around_animation</code>).
    /// See <a href="https://corefork.telegram.org/type/Document" />
    ///</summary>
    MyTelegram.Schema.IDocument? CenterIcon { get; set; }
}
