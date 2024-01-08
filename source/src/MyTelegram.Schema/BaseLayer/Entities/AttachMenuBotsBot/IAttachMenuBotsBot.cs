// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Represents a <a href="https://corefork.telegram.org/bots/webapps#launching-mini-apps-from-the-attachment-menu">bot mini app that can be launched from the attachment menu »</a>
/// See <a href="https://corefork.telegram.org/constructor/AttachMenuBotsBot" />
///</summary>
[JsonDerivedType(typeof(TAttachMenuBotsBot), nameof(TAttachMenuBotsBot))]
public interface IAttachMenuBotsBot : IObject
{
    ///<summary>
    /// Represents a <a href="https://corefork.telegram.org/api/bots/attach">bot mini app that can be launched from the attachment menu »</a><br>
    /// See <a href="https://corefork.telegram.org/type/AttachMenuBot" />
    ///</summary>
    MyTelegram.Schema.IAttachMenuBot Bot { get; set; }

    ///<summary>
    /// Info about related users and bots
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
