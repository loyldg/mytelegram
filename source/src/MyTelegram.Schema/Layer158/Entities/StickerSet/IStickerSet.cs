// ReSharper disable All

namespace MyTelegram.Schema;

public interface IStickerSet : IObject
{
    BitArray Flags { get; set; }
    bool Archived { get; set; }
    bool Official { get; set; }
    bool Masks { get; set; }
    bool Animated { get; set; }
    bool Videos { get; set; }
    bool Emojis { get; set; }
    int? InstalledDate { get; set; }
    long Id { get; set; }
    long AccessHash { get; set; }
    string Title { get; set; }
    string ShortName { get; set; }
    TVector<MyTelegram.Schema.IPhotoSize>? Thumbs { get; set; }
    int? ThumbDcId { get; set; }
    int? ThumbVersion { get; set; }
    long? ThumbDocumentId { get; set; }
    int Count { get; set; }
    int Hash { get; set; }
}
