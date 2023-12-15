﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Empty rich text element
/// See <a href="https://corefork.telegram.org/constructor/textEmpty" />
///</summary>
[TlObject(0xdc3d824f)]
public sealed class TTextEmpty : IRichText,IEmpty
{
    public uint ConstructorId => 0xdc3d824f;


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