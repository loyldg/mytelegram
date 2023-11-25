// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Represents an attachment menu icon for <a href="https://corefork.telegram.org/bots/webapps#launching-web-apps-from-the-attachment-menu">bot web apps »</a>
/// See <a href="https://corefork.telegram.org/constructor/AttachMenuBotIcon" />
///</summary>
[JsonDerivedType(typeof(TAttachMenuBotIcon), nameof(TAttachMenuBotIcon))]
public interface IAttachMenuBotIcon : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// One of the following values: note that animated icons must be played when the user clicks on the button, activating the bot web app. <br><br><code>default_static</code> - Default attachment menu icon in SVG format <br><code>placeholder_static</code> - Default placeholder for opened Web Apps in SVG format <br><code>ios_static</code> - Attachment menu icon in SVG format for the official iOS app <br><code>ios_animated</code> - Animated attachment menu icon in TGS format for the official iOS app <br><code>android_animated</code> - Animated attachment menu icon in TGS format for the official Android app <br><code>macos_animated</code> - Animated attachment menu icon in TGS format for the official native Mac OS app
    ///</summary>
    string Name { get; set; }

    ///<summary>
    /// The actual icon file.
    /// See <a href="https://corefork.telegram.org/type/Document" />
    ///</summary>
    MyTelegram.Schema.IDocument Icon { get; set; }

    ///<summary>
    /// Attachment menu icon colors.
    /// See <a href="https://corefork.telegram.org/type/AttachMenuBotIconColor" />
    ///</summary>
    TVector<MyTelegram.Schema.IAttachMenuBotIconColor>? Colors { get; set; }
}
