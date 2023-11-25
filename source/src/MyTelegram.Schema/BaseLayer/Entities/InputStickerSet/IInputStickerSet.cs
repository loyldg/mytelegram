// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Represents a stickerset
/// See <a href="https://corefork.telegram.org/constructor/InputStickerSet" />
///</summary>
[JsonDerivedType(typeof(TInputStickerSetEmpty), nameof(TInputStickerSetEmpty))]
[JsonDerivedType(typeof(TInputStickerSetID), nameof(TInputStickerSetID))]
[JsonDerivedType(typeof(TInputStickerSetShortName), nameof(TInputStickerSetShortName))]
[JsonDerivedType(typeof(TInputStickerSetAnimatedEmoji), nameof(TInputStickerSetAnimatedEmoji))]
[JsonDerivedType(typeof(TInputStickerSetDice), nameof(TInputStickerSetDice))]
[JsonDerivedType(typeof(TInputStickerSetAnimatedEmojiAnimations), nameof(TInputStickerSetAnimatedEmojiAnimations))]
[JsonDerivedType(typeof(TInputStickerSetPremiumGifts), nameof(TInputStickerSetPremiumGifts))]
[JsonDerivedType(typeof(TInputStickerSetEmojiGenericAnimations), nameof(TInputStickerSetEmojiGenericAnimations))]
[JsonDerivedType(typeof(TInputStickerSetEmojiDefaultStatuses), nameof(TInputStickerSetEmojiDefaultStatuses))]
[JsonDerivedType(typeof(TInputStickerSetEmojiDefaultTopicIcons), nameof(TInputStickerSetEmojiDefaultTopicIcons))]
public interface IInputStickerSet : IObject
{

}
