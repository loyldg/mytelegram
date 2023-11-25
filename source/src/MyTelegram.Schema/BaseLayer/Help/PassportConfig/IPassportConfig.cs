// ReSharper disable All

namespace MyTelegram.Schema.Help;

///<summary>
/// Telegram <a href="https://corefork.telegram.org/passport">passport</a> configuration
/// See <a href="https://corefork.telegram.org/constructor/help.PassportConfig" />
///</summary>
[JsonDerivedType(typeof(TPassportConfigNotModified), nameof(TPassportConfigNotModified))]
[JsonDerivedType(typeof(TPassportConfig), nameof(TPassportConfig))]
public interface IPassportConfig : IObject
{

}
