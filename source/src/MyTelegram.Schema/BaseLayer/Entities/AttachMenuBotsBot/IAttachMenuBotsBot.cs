// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Represents a <a href="https://corefork.telegram.org/bots/webapps#launching-web-apps-from-the-attachment-menu">bot web app that can be launched from the attachment menu »</a>
/// See <a href="https://corefork.telegram.org/constructor/AttachMenuBotsBot" />
///</summary>
public interface IAttachMenuBotsBot : IObject
{
    ///<summary>
    /// Represents a <a href="https://corefork.telegram.org/api/bots/attach">bot web app that can be launched from the attachment menu »</a><br>
    /// See <a href="https://corefork.telegram.org/type/AttachMenuBot" />
    ///</summary>
    MyTelegram.Schema.IAttachMenuBot Bot { get; set; }

    ///<summary>
    /// Info about related users and bots
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
