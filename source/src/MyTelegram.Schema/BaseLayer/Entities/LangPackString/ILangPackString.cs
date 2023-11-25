// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Language pack string
/// See <a href="https://corefork.telegram.org/constructor/LangPackString" />
///</summary>
[JsonDerivedType(typeof(TLangPackString), nameof(TLangPackString))]
[JsonDerivedType(typeof(TLangPackStringPluralized), nameof(TLangPackStringPluralized))]
[JsonDerivedType(typeof(TLangPackStringDeleted), nameof(TLangPackStringDeleted))]
public interface ILangPackString : IObject
{
    ///<summary>
    /// Localization key
    ///</summary>
    string Key { get; set; }
}
