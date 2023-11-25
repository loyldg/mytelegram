// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Indicates an already sent game
/// See <a href="https://corefork.telegram.org/constructor/Game" />
///</summary>
[JsonDerivedType(typeof(TGame), nameof(TGame))]
public interface IGame : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// ID of the game
    ///</summary>
    long Id { get; set; }

    ///<summary>
    /// Access hash of the game
    ///</summary>
    long AccessHash { get; set; }

    ///<summary>
    /// Short name for the game
    ///</summary>
    string ShortName { get; set; }

    ///<summary>
    /// Title of the game
    ///</summary>
    string Title { get; set; }

    ///<summary>
    /// Game description
    ///</summary>
    string Description { get; set; }

    ///<summary>
    /// Game preview
    /// See <a href="https://corefork.telegram.org/type/Photo" />
    ///</summary>
    MyTelegram.Schema.IPhoto Photo { get; set; }

    ///<summary>
    /// Optional attached document
    /// See <a href="https://corefork.telegram.org/type/Document" />
    ///</summary>
    MyTelegram.Schema.IDocument? Document { get; set; }
}
