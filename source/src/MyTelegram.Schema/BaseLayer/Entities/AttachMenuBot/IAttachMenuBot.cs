// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Represents a <a href="https://corefork.telegram.org/bots/webapps#launching-mini-apps-from-the-attachment-menu">bot mini app that can be launched from the attachment menu »</a>
/// See <a href="https://corefork.telegram.org/constructor/AttachMenuBot" />
///</summary>
[JsonDerivedType(typeof(TAttachMenuBot), nameof(TAttachMenuBot))]
public interface IAttachMenuBot : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// If set, before launching the mini app the client should ask the user to add the mini app to the attachment/side menu, and only if the user accepts, after invoking <a href="https://corefork.telegram.org/method/messages.toggleBotInAttachMenu">messages.toggleBotInAttachMenu</a> the app should be opened.
    ///</summary>
    bool Inactive { get; set; }

    ///<summary>
    /// Deprecated flag, can be ignored.
    ///</summary>
    bool HasSettings { get; set; }

    ///<summary>
    /// Whether the bot would like to send messages to the user.
    ///</summary>
    bool RequestWriteAccess { get; set; }

    ///<summary>
    /// Whether, when installed, an attachment menu entry should be shown for the Mini App.
    ///</summary>
    bool ShowInAttachMenu { get; set; }

    ///<summary>
    /// Whether, when installed, an entry in the main view side menu should be shown for the Mini App.
    ///</summary>
    bool ShowInSideMenu { get; set; }

    ///<summary>
    /// If <code>inactive</code> if set and the user hasn't previously accepted the third-party mini apps <a href="https://telegram.org/tos/mini-apps">Terms of Service</a> for this bot, when showing the mini app installation prompt, an additional mandatory checkbox to accept the <a href="https://telegram.org/tos/mini-apps">mini apps TOS</a> and a disclaimer indicating that this Mini App is not affiliated to Telegram should be shown.
    ///</summary>
    bool SideMenuDisclaimerNeeded { get; set; }

    ///<summary>
    /// Bot ID
    ///</summary>
    long BotId { get; set; }

    ///<summary>
    /// Attachment menu item name
    ///</summary>
    string ShortName { get; set; }

    ///<summary>
    /// List of dialog types where this attachment menu entry should be shown
    /// See <a href="https://corefork.telegram.org/type/AttachMenuPeerType" />
    ///</summary>
    TVector<MyTelegram.Schema.IAttachMenuPeerType>? PeerTypes { get; set; }

    ///<summary>
    /// List of platform-specific static icons and animations to use for the attachment menu button
    /// See <a href="https://corefork.telegram.org/type/AttachMenuBotIcon" />
    ///</summary>
    TVector<MyTelegram.Schema.IAttachMenuBotIcon> Icons { get; set; }
}
