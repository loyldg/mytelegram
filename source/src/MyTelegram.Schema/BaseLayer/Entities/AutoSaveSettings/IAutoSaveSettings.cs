// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Media autosave settings
/// See <a href="https://corefork.telegram.org/constructor/AutoSaveSettings" />
///</summary>
public interface IAutoSaveSettings : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Whether photos should be autosaved to the gallery.
    ///</summary>
    bool Photos { get; set; }

    ///<summary>
    /// Whether videos should be autosaved to the gallery.
    ///</summary>
    bool Videos { get; set; }

    ///<summary>
    /// If set, specifies a size limit for autosavable videos
    ///</summary>
    long? VideoMaxSize { get; set; }
}
