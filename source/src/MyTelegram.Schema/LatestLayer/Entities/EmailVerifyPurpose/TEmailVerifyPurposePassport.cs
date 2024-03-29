﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Verify an email for use in <a href="https://corefork.telegram.org/api/passport">telegram passport</a>
/// See <a href="https://corefork.telegram.org/constructor/emailVerifyPurposePassport" />
///</summary>
[TlObject(0xbbf51685)]
public sealed class TEmailVerifyPurposePassport : IEmailVerifyPurpose
{
    public uint ConstructorId => 0xbbf51685;


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