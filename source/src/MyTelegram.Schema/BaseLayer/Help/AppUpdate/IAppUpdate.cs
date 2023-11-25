// ReSharper disable All

namespace MyTelegram.Schema.Help;

///<summary>
/// Contains info on app update availability.
/// See <a href="https://corefork.telegram.org/constructor/help.AppUpdate" />
///</summary>
[JsonDerivedType(typeof(TAppUpdate), nameof(TAppUpdate))]
[JsonDerivedType(typeof(TNoAppUpdate), nameof(TNoAppUpdate))]
public interface IAppUpdate : IObject
{

}
