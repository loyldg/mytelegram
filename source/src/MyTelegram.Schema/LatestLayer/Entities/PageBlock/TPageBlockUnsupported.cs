﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Unsupported IV element
/// See <a href="https://corefork.telegram.org/constructor/pageBlockUnsupported" />
///</summary>
[TlObject(0x13567e8a)]
public sealed class TPageBlockUnsupported : IPageBlock
{
    public uint ConstructorId => 0x13567e8a;


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