// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/PublicForward" />
///</summary>
[JsonDerivedType(typeof(TPublicForwardMessage), nameof(TPublicForwardMessage))]
[JsonDerivedType(typeof(TPublicForwardStory), nameof(TPublicForwardStory))]
public interface IPublicForward : IObject
{

}
