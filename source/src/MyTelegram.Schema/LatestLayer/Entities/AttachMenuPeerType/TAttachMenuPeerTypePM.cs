﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// The bot attachment menu entry is available in private chats with other users (not bots)
/// See <a href="https://corefork.telegram.org/constructor/attachMenuPeerTypePM" />
///</summary>
[TlObject(0xf146d31f)]
public sealed class TAttachMenuPeerTypePM : IAttachMenuPeerType
{
    public uint ConstructorId => 0xf146d31f;


    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);

    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {

    }
}