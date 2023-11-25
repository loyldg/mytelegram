// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Related articles
/// See <a href="https://corefork.telegram.org/constructor/PageRelatedArticle" />
///</summary>
[JsonDerivedType(typeof(TPageRelatedArticle), nameof(TPageRelatedArticle))]
public interface IPageRelatedArticle : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// URL of article
    ///</summary>
    string Url { get; set; }

    ///<summary>
    /// Webpage ID of generated IV preview
    ///</summary>
    long WebpageId { get; set; }

    ///<summary>
    /// Title
    ///</summary>
    string? Title { get; set; }

    ///<summary>
    /// Description
    ///</summary>
    string? Description { get; set; }

    ///<summary>
    /// ID of preview photo
    ///</summary>
    long? PhotoId { get; set; }

    ///<summary>
    /// Author name
    ///</summary>
    string? Author { get; set; }

    ///<summary>
    /// Date of publication
    ///</summary>
    int? PublishedDate { get; set; }
}
