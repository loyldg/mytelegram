﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// The bot attachment menu entry is available in <a href="https://corefork.telegram.org/api/channel">groups and supergroups</a>
/// See <a href="https://corefork.telegram.org/constructor/attachMenuPeerTypeChat" />
///</summary>
[TlObject(0x509113f)]
public sealed class TAttachMenuPeerTypeChat : IAttachMenuPeerType
{
    public uint ConstructorId => 0x509113f;


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