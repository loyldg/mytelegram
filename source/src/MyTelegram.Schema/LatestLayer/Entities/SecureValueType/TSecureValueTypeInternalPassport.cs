﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Internal <a href="https://corefork.telegram.org/passport">passport</a>
/// See <a href="https://corefork.telegram.org/constructor/secureValueTypeInternalPassport" />
///</summary>
[TlObject(0x99a48f23)]
public sealed class TSecureValueTypeInternalPassport : ISecureValueType
{
    public uint ConstructorId => 0x99a48f23;


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