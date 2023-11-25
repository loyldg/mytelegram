// ReSharper disable All

namespace MyTelegram.Schema.Account;

///<summary>
/// Contains information about a saved notification sound
/// See <a href="https://corefork.telegram.org/constructor/account.SavedRingtone" />
///</summary>
[JsonDerivedType(typeof(TSavedRingtone), nameof(TSavedRingtone))]
[JsonDerivedType(typeof(TSavedRingtoneConverted), nameof(TSavedRingtoneConverted))]
public interface ISavedRingtone : IObject
{

}
