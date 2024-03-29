﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// The stickerset was installed, but since there are too many stickersets some were archived
/// See <a href="https://corefork.telegram.org/constructor/messages.stickerSetInstallResultArchive" />
///</summary>
[TlObject(0x35e410a8)]
public sealed class TStickerSetInstallResultArchive : IStickerSetInstallResult
{
    public uint ConstructorId => 0x35e410a8;
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
        writer.Write(Sets);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Sets = reader.Read<TVector<MyTelegram.Schema.IStickerSetCovered>>();
    }
}