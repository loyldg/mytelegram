// ReSharper disable All

namespace MyTelegram.Schema.Account;

///<summary>
/// Contains a list of saved notification sounds
/// See <a href="https://corefork.telegram.org/constructor/account.SavedRingtones" />
///</summary>
[JsonDerivedType(typeof(TSavedRingtonesNotModified), nameof(TSavedRingtonesNotModified))]
[JsonDerivedType(typeof(TSavedRingtones), nameof(TSavedRingtones))]
public interface ISavedRingtones : IObject
{

}
