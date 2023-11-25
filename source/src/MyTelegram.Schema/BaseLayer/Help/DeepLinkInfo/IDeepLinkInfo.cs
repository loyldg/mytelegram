// ReSharper disable All

namespace MyTelegram.Schema.Help;

///<summary>
/// Contains information about an unsupported <a href="https://corefork.telegram.org/api/links#unsupported-links">deep link »</a>
/// See <a href="https://corefork.telegram.org/constructor/help.DeepLinkInfo" />
///</summary>
[JsonDerivedType(typeof(TDeepLinkInfoEmpty), nameof(TDeepLinkInfoEmpty))]
[JsonDerivedType(typeof(TDeepLinkInfo), nameof(TDeepLinkInfo))]
public interface IDeepLinkInfo : IObject
{

}
