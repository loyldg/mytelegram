// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Basic theme settings
/// See <a href="https://corefork.telegram.org/constructor/BaseTheme" />
///</summary>
[JsonDerivedType(typeof(TBaseThemeClassic), nameof(TBaseThemeClassic))]
[JsonDerivedType(typeof(TBaseThemeDay), nameof(TBaseThemeDay))]
[JsonDerivedType(typeof(TBaseThemeNight), nameof(TBaseThemeNight))]
[JsonDerivedType(typeof(TBaseThemeTinted), nameof(TBaseThemeTinted))]
[JsonDerivedType(typeof(TBaseThemeArctic), nameof(TBaseThemeArctic))]
public interface IBaseTheme : IObject
{

}
