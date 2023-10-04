// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Language pack language
/// See <a href="https://corefork.telegram.org/constructor/LangPackLanguage" />
///</summary>
public interface ILangPackLanguage : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Whether the language pack is official
    ///</summary>
    bool Official { get; set; }

    ///<summary>
    /// Is this a localization pack for an RTL language
    ///</summary>
    bool Rtl { get; set; }

    ///<summary>
    /// Is this a beta localization pack?
    ///</summary>
    bool Beta { get; set; }

    ///<summary>
    /// Language name
    ///</summary>
    string Name { get; set; }

    ///<summary>
    /// Language name in the language itself
    ///</summary>
    string NativeName { get; set; }

    ///<summary>
    /// Language code (pack identifier)
    ///</summary>
    string LangCode { get; set; }

    ///<summary>
    /// Identifier of a base language pack; may be empty. If a string is missed in the language pack, then it should be fetched from base language pack. Unsupported in custom language packs
    ///</summary>
    string? BaseLangCode { get; set; }

    ///<summary>
    /// A language code to be used to apply plural forms. See <a href="https://www.unicode.org/cldr/charts/latest/supplemental/language_plural_rules.html">https://www.unicode.org/cldr/charts/latest/supplemental/language_plural_rules.html</a> for more info
    ///</summary>
    string PluralCode { get; set; }

    ///<summary>
    /// Total number of non-deleted strings from the language pack
    ///</summary>
    int StringsCount { get; set; }

    ///<summary>
    /// Total number of translated strings from the language pack
    ///</summary>
    int TranslatedCount { get; set; }

    ///<summary>
    /// Link to language translation interface; empty for custom local language packs
    ///</summary>
    string TranslationsUrl { get; set; }
}
