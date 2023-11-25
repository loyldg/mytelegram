// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Notifications settings.
/// See <a href="https://corefork.telegram.org/constructor/InputPeerNotifySettings" />
///</summary>
[JsonDerivedType(typeof(TInputPeerNotifySettings), nameof(TInputPeerNotifySettings))]
public interface IInputPeerNotifySettings : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// If the text of the message shall be displayed in notification
    /// See <a href="https://corefork.telegram.org/type/Bool" />
    ///</summary>
    bool? ShowPreviews { get; set; }

    ///<summary>
    /// Peer was muted?
    /// See <a href="https://corefork.telegram.org/type/Bool" />
    ///</summary>
    bool? Silent { get; set; }

    ///<summary>
    /// Date until which all notifications shall be switched off
    ///</summary>
    int? MuteUntil { get; set; }

    ///<summary>
    /// Name of an audio file for notification
    /// See <a href="https://corefork.telegram.org/type/NotificationSound" />
    ///</summary>
    MyTelegram.Schema.INotificationSound? Sound { get; set; }

    ///<summary>
    /// &nbsp;
    /// See <a href="https://corefork.telegram.org/type/Bool" />
    ///</summary>
    bool? StoriesMuted { get; set; }

    ///<summary>
    /// &nbsp;
    /// See <a href="https://corefork.telegram.org/type/Bool" />
    ///</summary>
    bool? StoriesHideSender { get; set; }

    ///<summary>
    /// &nbsp;
    /// See <a href="https://corefork.telegram.org/type/NotificationSound" />
    ///</summary>
    MyTelegram.Schema.INotificationSound? StoriesSound { get; set; }
}
