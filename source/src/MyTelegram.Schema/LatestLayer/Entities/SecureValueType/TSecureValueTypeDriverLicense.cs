﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Driver's license
/// See <a href="https://corefork.telegram.org/constructor/secureValueTypeDriverLicense" />
///</summary>
[TlObject(0x6e425c4)]
public sealed class TSecureValueTypeDriverLicense : ISecureValueType
{
    public uint ConstructorId => 0x6e425c4;


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