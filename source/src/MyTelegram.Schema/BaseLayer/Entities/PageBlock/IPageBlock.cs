// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Represents an <a href="https://instantview.telegram.org/">instant view page element</a>
/// See <a href="https://corefork.telegram.org/constructor/PageBlock" />
///</summary>
[JsonDerivedType(typeof(TPageBlockUnsupported), nameof(TPageBlockUnsupported))]
[JsonDerivedType(typeof(TPageBlockTitle), nameof(TPageBlockTitle))]
[JsonDerivedType(typeof(TPageBlockSubtitle), nameof(TPageBlockSubtitle))]
[JsonDerivedType(typeof(TPageBlockAuthorDate), nameof(TPageBlockAuthorDate))]
[JsonDerivedType(typeof(TPageBlockHeader), nameof(TPageBlockHeader))]
[JsonDerivedType(typeof(TPageBlockSubheader), nameof(TPageBlockSubheader))]
[JsonDerivedType(typeof(TPageBlockParagraph), nameof(TPageBlockParagraph))]
[JsonDerivedType(typeof(TPageBlockPreformatted), nameof(TPageBlockPreformatted))]
[JsonDerivedType(typeof(TPageBlockFooter), nameof(TPageBlockFooter))]
[JsonDerivedType(typeof(TPageBlockDivider), nameof(TPageBlockDivider))]
[JsonDerivedType(typeof(TPageBlockAnchor), nameof(TPageBlockAnchor))]
[JsonDerivedType(typeof(TPageBlockList), nameof(TPageBlockList))]
[JsonDerivedType(typeof(TPageBlockBlockquote), nameof(TPageBlockBlockquote))]
[JsonDerivedType(typeof(TPageBlockPullquote), nameof(TPageBlockPullquote))]
[JsonDerivedType(typeof(TPageBlockPhoto), nameof(TPageBlockPhoto))]
[JsonDerivedType(typeof(TPageBlockVideo), nameof(TPageBlockVideo))]
[JsonDerivedType(typeof(TPageBlockCover), nameof(TPageBlockCover))]
[JsonDerivedType(typeof(TPageBlockEmbed), nameof(TPageBlockEmbed))]
[JsonDerivedType(typeof(TPageBlockEmbedPost), nameof(TPageBlockEmbedPost))]
[JsonDerivedType(typeof(TPageBlockCollage), nameof(TPageBlockCollage))]
[JsonDerivedType(typeof(TPageBlockSlideshow), nameof(TPageBlockSlideshow))]
[JsonDerivedType(typeof(TPageBlockChannel), nameof(TPageBlockChannel))]
[JsonDerivedType(typeof(TPageBlockAudio), nameof(TPageBlockAudio))]
[JsonDerivedType(typeof(TPageBlockKicker), nameof(TPageBlockKicker))]
[JsonDerivedType(typeof(TPageBlockTable), nameof(TPageBlockTable))]
[JsonDerivedType(typeof(TPageBlockOrderedList), nameof(TPageBlockOrderedList))]
[JsonDerivedType(typeof(TPageBlockDetails), nameof(TPageBlockDetails))]
[JsonDerivedType(typeof(TPageBlockRelatedArticles), nameof(TPageBlockRelatedArticles))]
[JsonDerivedType(typeof(TPageBlockMap), nameof(TPageBlockMap))]
public interface IPageBlock : IObject
{

}
