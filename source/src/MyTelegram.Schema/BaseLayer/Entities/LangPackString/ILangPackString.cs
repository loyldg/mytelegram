// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Language pack string
/// See <a href="https://corefork.telegram.org/constructor/LangPackString" />
///</summary>
public interface ILangPackString : IObject
{
    ///<summary>
    /// Localization key
    ///</summary>
    string Key { get; set; }
}
