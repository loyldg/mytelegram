﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Personal details
/// See <a href="https://corefork.telegram.org/constructor/secureValueTypePersonalDetails" />
///</summary>
[TlObject(0x9d2a81e3)]
public sealed class TSecureValueTypePersonalDetails : ISecureValueType
{
    public uint ConstructorId => 0x9d2a81e3;


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