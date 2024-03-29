﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// All peers in a <a href="https://corefork.telegram.org/api/folders#peer-folders">peer folder</a>
/// See <a href="https://corefork.telegram.org/constructor/inputDialogPeerFolder" />
///</summary>
[TlObject(0x64600527)]
public sealed class TInputDialogPeerFolder : IInputDialogPeer
{
    public uint ConstructorId => 0x64600527;
    ///<summary>
    /// <a href="https://corefork.telegram.org/api/folders#peer-folders">Peer folder ID, for more info click here</a>
    ///</summary>
    public int FolderId { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(FolderId);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        FolderId = reader.ReadInt32();
    }
}