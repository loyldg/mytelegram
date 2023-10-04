// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Represents a <a href="https://corefork.telegram.org/bots/webapps#launching-web-apps-from-the-attachment-menu">bot web app that can be launched from the attachment menu »</a>
/// See <a href="https://corefork.telegram.org/constructor/AttachMenuBot" />
///</summary>
public interface IAttachMenuBot : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Whether this bot attachment menu entry should be shown in the attachment menu (toggle using <a href="https://corefork.telegram.org/method/messages.toggleBotInAttachMenu">messages.toggleBotInAttachMenu</a>)
    ///</summary>
    bool Inactive { get; set; }

    ///<summary>
    /// True, if the bot supports the <a href="https://corefork.telegram.org/api/bots/webapps#settings-button-pressed">"settings_button_pressed" event »</a>
    ///</summary>
    bool HasSettings { get; set; }

    ///<summary>
    /// Whether the bot would like to send messages to the user.
    ///</summary>
    bool RequestWriteAccess { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    bool ShowInAttachMenu { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    bool ShowInSideMenu { get; set; }

    ///<summary>
    /// &nbsp;
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
