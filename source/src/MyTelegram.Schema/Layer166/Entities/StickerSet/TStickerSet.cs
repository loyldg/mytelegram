﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Represents a stickerset (stickerpack)
/// See <a href="https://corefork.telegram.org/constructor/stickerSet" />
///</summary>
[TlObject(0x2dd14edc)]
public sealed class TStickerSet : IStickerSet
{
    public uint ConstructorId => 0x2dd14edc;
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    public BitArray Flags { get; set; } = new BitArray(32);

    ///<summary>
    /// Whether this stickerset was archived (due to too many saved stickers in the current account)
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool Archived { get; set; }

    ///<summary>
    /// Is this stickerset official
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool Official { get; set; }

    ///<summary>
    /// Is this a mask stickerset
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool Masks { get; set; }

    ///<summary>
    /// Is this an animated stickerpack
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool Animated { get; set; }

    ///<summary>
    /// Is this a video stickerpack
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool Videos { get; set; }

    ///<summary>
    /// This is a custom emoji stickerset
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool Emojis { get; set; }
    public bool TextColor { get; set; }

    ///<summary>
    /// When was this stickerset installed
    ///</summary>
    public int? InstalledDate { get; set; }

    ///<summary>
    /// ID of the stickerset
    ///</summary>
    public long Id { get; set; }

    ///<summary>
    /// Access hash of stickerset
    ///</summary>
    public long AccessHash { get; set; }

    ///<summary>
    /// Title of stickerset
    ///</summary>
    public string Title { get; set; }

    ///<summary>
    /// Short name of stickerset, used when sharing stickerset using <a href="https://corefork.telegram.org/api/links#stickerset-links">stickerset deep links</a>.
    ///</summary>
    public string ShortName { get; set; }

    ///<summary>
    /// Stickerset thumbnail
    ///</summary>
    public TVector<MyTelegram.Schema.IPhotoSize>? Thumbs { get; set; }

    ///<summary>
    /// DC ID of thumbnail
    ///</summary>
    public int? ThumbDcId { get; set; }

    ///<summary>
    /// Thumbnail version
    ///</summary>
    public int? ThumbVersion { get; set; }

    ///<summary>
    /// Document ID of custom emoji thumbnail, fetch the document using <a href="https://corefork.telegram.org/method/messages.getCustomEmojiDocuments">messages.getCustomEmojiDocuments</a>
    ///</summary>
    public long? ThumbDocumentId { get; set; }

    ///<summary>
    /// Number of stickers in pack
    ///</summary>
    public int Count { get; set; }

    ///<summary>
    /// Hash
    ///</summary>
    public int Hash { get; set; }

    public void ComputeFlag()
    {
        if (Archived) { Flags[1] = true; }
        if (Official) { Flags[2] = true; }
        if (Masks) { Flags[3] = true; }
        if (Animated) { Flags[5] = true; }
        if (Videos) { Flags[6] = true; }
        if (Emojis) { Flags[7] = true; }
        if (TextColor) { Flags[9] = true; }
        if (/*InstalledDate != 0 && */InstalledDate.HasValue) { Flags[0] = true; }
        if (Thumbs?.Count > 0) { Flags[4] = true; }
        if (/*ThumbDcId != 0 && */ThumbDcId.HasValue) { Flags[4] = true; }
        if (/*ThumbVersion != 0 && */ThumbVersion.HasValue) { Flags[4] = true; }
        if (/*ThumbDocumentId != 0 &&*/ ThumbDocumentId.HasValue) { Flags[8] = true; }

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        if (Flags[0]) { writer.Write(InstalledDate.Value); }
        writer.Write(Id);
        writer.Write(AccessHash);
        writer.Write(Title);
        writer.Write(ShortName);
        if (Flags[4]) { writer.Write(Thumbs); }
        if (Flags[4]) { writer.Write(ThumbDcId.Value); }
        if (Flags[4]) { writer.Write(ThumbVersion.Value); }
        if (Flags[8]) { writer.Write(ThumbDocumentId.Value); }
        writer.Write(Count);
        writer.Write(Hash);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        if (Flags[1]) { Archived = true; }
        if (Flags[2]) { Official = true; }
        if (Flags[3]) { Masks = true; }
        if (Flags[5]) { Animated = true; }
        if (Flags[6]) { Videos = true; }
        if (Flags[7]) { Emojis = true; }
        if (Flags[9]) { TextColor = true; }
        if (Flags[0]) { InstalledDate = reader.ReadInt32(); }
        Id = reader.ReadInt64();
        AccessHash = reader.ReadInt64();
        Title = reader.ReadString();
        ShortName = reader.ReadString();
        if (Flags[4]) { Thumbs = reader.Read<TVector<MyTelegram.Schema.IPhotoSize>>(); }
        if (Flags[4]) { ThumbDcId = reader.ReadInt32(); }
        if (Flags[4]) { ThumbVersion = reader.ReadInt32(); }
        if (Flags[8]) { ThumbDocumentId = reader.ReadInt64(); }
        Count = reader.ReadInt32();
        Hash = reader.ReadInt32();
    }
}