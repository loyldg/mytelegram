// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Represents a stickerset (stickerpack)
/// See <a href="https://corefork.telegram.org/constructor/StickerSet" />
///</summary>
public interface IStickerSet : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Whether this stickerset was archived (due to too many saved stickers in the current account)
    ///</summary>
    bool Archived { get; set; }

    ///<summary>
    /// Is this stickerset official
    ///</summary>
    bool Official { get; set; }

    ///<summary>
    /// Is this a mask stickerset
    ///</summary>
    bool Masks { get; set; }

    ///<summary>
    /// Is this an animated stickerpack
    ///</summary>
    bool Animated { get; set; }

    ///<summary>
    /// Is this a video stickerpack
    ///</summary>
    bool Videos { get; set; }

    ///<summary>
    /// This is a custom emoji stickerset
    ///</summary>
    bool Emojis { get; set; }
    bool TextColor { get; set; }

    ///<summary>
    /// When was this stickerset installed
    ///</summary>
    int? InstalledDate { get; set; }

    ///<summary>
    /// ID of the stickerset
    ///</summary>
    long Id { get; set; }

    ///<summary>
    /// Access hash of stickerset
    ///</summary>
    long AccessHash { get; set; }

    ///<summary>
    /// Title of stickerset
    ///</summary>
    string Title { get; set; }

    ///<summary>
    /// Short name of stickerset, used when sharing stickerset using <a href="https://corefork.telegram.org/api/links#stickerset-links">stickerset deep links</a>.
    ///</summary>
    string ShortName { get; set; }

    ///<summary>
    /// Stickerset thumbnail
    /// See <a href="https://corefork.telegram.org/type/PhotoSize" />
    ///</summary>
    TVector<MyTelegram.Schema.IPhotoSize>? Thumbs { get; set; }

    ///<summary>
    /// DC ID of thumbnail
    ///</summary>
    int? ThumbDcId { get; set; }

    ///<summary>
    /// Thumbnail version
    ///</summary>
    int? ThumbVersion { get; set; }

    ///<summary>
    /// Document ID of custom emoji thumbnail, fetch the document using <a href="https://corefork.telegram.org/method/messages.getCustomEmojiDocuments">messages.getCustomEmojiDocuments</a>
    ///</summary>
    long? ThumbDocumentId { get; set; }

    ///<summary>
    /// Number of stickers in pack
    ///</summary>
    int Count { get; set; }

    ///<summary>
    /// Hash
    ///</summary>
    int Hash { get; set; }
}
