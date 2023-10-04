// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Represents an attachment menu icon color for <a href="https://corefork.telegram.org/bots/webapps#launching-web-apps-from-the-attachment-menu">bot web apps »</a>
/// See <a href="https://corefork.telegram.org/constructor/AttachMenuBotIconColor" />
///</summary>
public interface IAttachMenuBotIconColor : IObject
{
    ///<summary>
    /// One of the following values: <br><code>light_icon</code> - Color of the attachment menu icon (light mode) <br><code>light_text</code> - Color of the attachment menu label, once selected (light mode) <br><code>dark_icon</code> - Color of the attachment menu icon (dark mode) <br><code>dark_text</code> - Color of the attachment menu label, once selected (dark mode)
    ///</summary>
    string Name { get; set; }

    ///<summary>
    /// Color in RGB24 format
    ///</summary>
    int Color { get; set; }
}
