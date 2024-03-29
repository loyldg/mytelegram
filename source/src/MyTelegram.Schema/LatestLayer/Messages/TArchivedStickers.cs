﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Archived stickersets
/// See <a href="https://corefork.telegram.org/constructor/messages.archivedStickers" />
///</summary>
[TlObject(0x4fcba9c8)]
public sealed class TArchivedStickers : IArchivedStickers
{
    public uint ConstructorId => 0x4fcba9c8;
    ///<summary>
    /// Number of archived stickers
    ///</summary>
    public int Count { get; set; }

    ///<summary>
    /// Archived stickersets
    ///</summary>
    public TVector<MyTelegram.Schema.IStickerSetCovered> Sets { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Count);
        writer.Write(Sets);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Count = reader.ReadInt32();
        Sets = reader.Read<TVector<MyTelegram.Schema.IStickerSetCovered>>();
    }
}