﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Most used inline bots
/// See <a href="https://corefork.telegram.org/constructor/topPeerCategoryBotsInline" />
///</summary>
[TlObject(0x148677e2)]
public sealed class TTopPeerCategoryBotsInline : ITopPeerCategory
{
    public uint ConstructorId => 0x148677e2;


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