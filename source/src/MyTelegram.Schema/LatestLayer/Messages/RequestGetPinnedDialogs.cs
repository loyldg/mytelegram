﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Get pinned dialogs
/// <para>Possible errors</para>
/// Code Type Description
/// 400 FOLDER_ID_INVALID Invalid folder ID.
/// See <a href="https://corefork.telegram.org/method/messages.getPinnedDialogs" />
///</summary>
[TlObject(0xd6b94df2)]
public sealed class RequestGetPinnedDialogs : IRequest<MyTelegram.Schema.Messages.IPeerDialogs>
{
    public uint ConstructorId => 0xd6b94df2;
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
