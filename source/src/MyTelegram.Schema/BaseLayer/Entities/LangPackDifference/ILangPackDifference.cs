// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Language pack changes
/// See <a href="https://corefork.telegram.org/constructor/LangPackDifference" />
///</summary>
public interface ILangPackDifference : IObject
{
    ///<summary>
    /// Language code
    ///</summary>
    string LangCode { get; set; }

    ///<summary>
    /// Previous version number
    ///</summary>
    int FromVersion { get; set; }

    ///<summary>
    /// New version number
    ///</summary>
    int Version { get; set; }

    ///<summary>
    /// Localized strings
    /// See <a href="https://corefork.telegram.org/type/LangPackString" />
    ///</summary>
    TVector<MyTelegram.Schema.ILangPackString> Strings { get; set; }
}
