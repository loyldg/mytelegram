// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Notification settings.
/// See <a href="https://corefork.telegram.org/constructor/PeerNotifySettings" />
///</summary>
[JsonDerivedType(typeof(TPeerNotifySettings), nameof(TPeerNotifySettings))]
public interface IPeerNotifySettings : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// (Ternary value) If set, indicates whether or not to display previews of messages in notifications; otherwise the default behavior should be used.
    /// See <a href="https://corefork.telegram.org/type/Bool" />
    ///</summary>
    bool? ShowPreviews { get; set; }

    ///<summary>
    /// (Ternary value) If set, indicates whether to mute or unmute the peer; otherwise the default behavior should be used.
    /// See <a href="https://corefork.telegram.org/type/Bool" />
    ///</summary>
    bool? Silent { get; set; }

    ///<summary>
    /// Mute all notifications until this date
    ///</summary>
    int? MuteUntil { get; set; }

    ///<summary>
    /// Notification sound for the official iOS application
    /// See <a href="https://corefork.telegram.org/type/NotificationSound" />
    ///</summary>
    MyTelegram.Schema.INotificationSound? IosSound { get; set; }

    ///<summary>
    /// Notification sound for the official android application
    /// See <a href="https://corefork.telegram.org/type/NotificationSound" />
    ///</summary>
    MyTelegram.Schema.INotificationSound? AndroidSound { get; set; }

    ///<summary>
    /// Notification sound for other applications
    /// See <a href="https://corefork.telegram.org/type/NotificationSound" />
    ///</summary>
    MyTelegram.Schema.INotificationSound? OtherSound { get; set; }

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
    MyTelegram.Schema.INotificationSound? StoriesIosSound { get; set; }

    ///<summary>
    /// &nbsp;
    /// See <a href="https://corefork.telegram.org/type/NotificationSound" />
    ///</summary>
    MyTelegram.Schema.INotificationSound? StoriesAndroidSound { get; set; }

    ///<summary>
    /// &nbsp;
    /// See <a href="https://corefork.telegram.org/type/NotificationSound" />
    ///</summary>
    MyTelegram.Schema.INotificationSound? StoriesOtherSound { get; set; }
}
