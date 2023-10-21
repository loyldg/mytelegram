﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Help;

///<summary>
/// Get localized name of the telegram support user
/// See <a href="https://corefork.telegram.org/method/help.getSupportName" />
///</summary>
[TlObject(0xd360e72c)]
public sealed class RequestGetSupportName : IRequest<MyTelegram.Schema.Help.ISupportName>
{
    public uint ConstructorId => 0xd360e72c;

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