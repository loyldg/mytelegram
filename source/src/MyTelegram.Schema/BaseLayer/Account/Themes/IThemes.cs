// ReSharper disable All

namespace MyTelegram.Schema.Account;

///<summary>
/// Installed themes
/// See <a href="https://corefork.telegram.org/constructor/account.Themes" />
///</summary>
[JsonDerivedType(typeof(TThemesNotModified), nameof(TThemesNotModified))]
[JsonDerivedType(typeof(TThemes), nameof(TThemes))]
public interface IThemes : IObject
{

}
