// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Extended media
/// See <a href="https://corefork.telegram.org/constructor/MessageExtendedMedia" />
///</summary>
[JsonDerivedType(typeof(TMessageExtendedMediaPreview), nameof(TMessageExtendedMediaPreview))]
[JsonDerivedType(typeof(TMessageExtendedMedia), nameof(TMessageExtendedMedia))]
public interface IMessageExtendedMedia : IObject
{

}
