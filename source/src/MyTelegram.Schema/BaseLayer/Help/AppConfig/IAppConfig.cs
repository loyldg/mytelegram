// ReSharper disable All

namespace MyTelegram.Schema.Help;

///<summary>
/// Contains various <a href="https://corefork.telegram.org/api/config#client-configuration">client configuration parameters</a>
/// See <a href="https://corefork.telegram.org/constructor/help.AppConfig" />
///</summary>
[JsonDerivedType(typeof(TAppConfigNotModified), nameof(TAppConfigNotModified))]
[JsonDerivedType(typeof(TAppConfig), nameof(TAppConfig))]
public interface IAppConfig : IObject
{

}
