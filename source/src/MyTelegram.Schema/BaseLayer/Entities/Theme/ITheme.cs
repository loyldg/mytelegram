// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Cloud theme
/// See <a href="https://corefork.telegram.org/constructor/Theme" />
///</summary>
public interface ITheme : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Whether the current user is the creator of this theme
    ///</summary>
    bool Creator { get; set; }

    ///<summary>
    /// Whether this is the default theme
    ///</summary>
    bool Default { get; set; }

    ///<summary>
    /// Whether this theme is meant to be used as a <a href="https://telegram.org/blog/chat-themes-interactive-emoji-read-receipts">chat theme</a>
    ///</summary>
    bool ForChat { get; set; }

    ///<summary>
    /// Theme ID
    ///</summary>
    long Id { get; set; }

    ///<summary>
    /// Theme access hash
    ///</summary>
    long AccessHash { get; set; }

    ///<summary>
    /// Unique theme ID
    ///</summary>
    string Slug { get; set; }

    ///<summary>
    /// Theme name
    ///</summary>
    string Title { get; set; }

    ///<summary>
    /// Theme
    /// See <a href="https://corefork.telegram.org/type/Document" />
    ///</summary>
    MyTelegram.Schema.IDocument? Document { get; set; }

    ///<summary>
    /// Theme settings
    /// See <a href="https://corefork.telegram.org/type/ThemeSettings" />
    ///</summary>
    TVector<MyTelegram.Schema.IThemeSettings>? Settings { get; set; }

    ///<summary>
    /// Theme emoji
    ///</summary>
    string? Emoticon { get; set; }

    ///<summary>
    /// Installation count
    ///</summary>
    int? InstallsCount { get; set; }
}
