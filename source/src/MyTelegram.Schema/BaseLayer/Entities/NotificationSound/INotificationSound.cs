// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Represents a notification sound
/// See <a href="https://corefork.telegram.org/constructor/NotificationSound" />
///</summary>
[JsonDerivedType(typeof(TNotificationSoundDefault), nameof(TNotificationSoundDefault))]
[JsonDerivedType(typeof(TNotificationSoundNone), nameof(TNotificationSoundNone))]
[JsonDerivedType(typeof(TNotificationSoundLocal), nameof(TNotificationSoundLocal))]
[JsonDerivedType(typeof(TNotificationSoundRingtone), nameof(TNotificationSoundRingtone))]
public interface INotificationSound : IObject
{

}
