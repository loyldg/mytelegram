// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/StoriesStealthMode" />
///</summary>
public interface IStoriesStealthMode : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    int? ActiveUntilDate { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    int? CooldownUntilDate { get; set; }
}
