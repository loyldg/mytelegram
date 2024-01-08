// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// <a href="https://corefork.telegram.org/api/stories#stealth-mode">Story stealth mode status</a>
/// See <a href="https://corefork.telegram.org/constructor/StoriesStealthMode" />
///</summary>
[JsonDerivedType(typeof(TStoriesStealthMode), nameof(TStoriesStealthMode))]
public interface IStoriesStealthMode : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// The date up to which stealth mode will be active.
    ///</summary>
    int? ActiveUntilDate { get; set; }

    ///<summary>
    /// The date starting from which the user will be allowed to re-enable stealth mode again.
    ///</summary>
    int? CooldownUntilDate { get; set; }
}
