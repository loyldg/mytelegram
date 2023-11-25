// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Found stickersets
/// See <a href="https://corefork.telegram.org/constructor/messages.FoundStickerSets" />
///</summary>
[JsonDerivedType(typeof(TFoundStickerSetsNotModified), nameof(TFoundStickerSetsNotModified))]
[JsonDerivedType(typeof(TFoundStickerSets), nameof(TFoundStickerSets))]
public interface IFoundStickerSets : IObject
{

}
