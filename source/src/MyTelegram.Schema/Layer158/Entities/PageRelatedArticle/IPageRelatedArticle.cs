// ReSharper disable All

namespace MyTelegram.Schema;

public interface IPageRelatedArticle : IObject
{
    BitArray Flags { get; set; }
    string Url { get; set; }
    long WebpageId { get; set; }
    string? Title { get; set; }
    string? Description { get; set; }
    long? PhotoId { get; set; }
    string? Author { get; set; }
    int? PublishedDate { get; set; }
}
