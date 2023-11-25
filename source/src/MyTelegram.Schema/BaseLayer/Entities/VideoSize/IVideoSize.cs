// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Represents an animated video thumbnail
/// See <a href="https://corefork.telegram.org/constructor/VideoSize" />
///</summary>
[JsonDerivedType(typeof(TVideoSize), nameof(TVideoSize))]
[JsonDerivedType(typeof(TVideoSizeEmojiMarkup), nameof(TVideoSizeEmojiMarkup))]
[JsonDerivedType(typeof(TVideoSizeStickerMarkup), nameof(TVideoSizeStickerMarkup))]
public interface IVideoSize : IObject
{

}
