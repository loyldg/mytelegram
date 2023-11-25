// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Various possible attributes of a document (used to define if it's a sticker, a GIF, a video, a mask sticker, an image, an audio, and so on)
/// See <a href="https://corefork.telegram.org/constructor/DocumentAttribute" />
///</summary>
[JsonDerivedType(typeof(TDocumentAttributeImageSize), nameof(TDocumentAttributeImageSize))]
[JsonDerivedType(typeof(TDocumentAttributeAnimated), nameof(TDocumentAttributeAnimated))]
[JsonDerivedType(typeof(TDocumentAttributeSticker), nameof(TDocumentAttributeSticker))]
[JsonDerivedType(typeof(TDocumentAttributeVideo), nameof(TDocumentAttributeVideo))]
[JsonDerivedType(typeof(TDocumentAttributeAudio), nameof(TDocumentAttributeAudio))]
[JsonDerivedType(typeof(TDocumentAttributeFilename), nameof(TDocumentAttributeFilename))]
[JsonDerivedType(typeof(TDocumentAttributeHasStickers), nameof(TDocumentAttributeHasStickers))]
[JsonDerivedType(typeof(TDocumentAttributeCustomEmoji), nameof(TDocumentAttributeCustomEmoji))]
public interface IDocumentAttribute : IObject
{

}
