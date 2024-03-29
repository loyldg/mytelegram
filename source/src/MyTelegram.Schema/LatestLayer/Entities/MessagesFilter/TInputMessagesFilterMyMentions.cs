﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Return only messages where the current user was <a href="https://corefork.telegram.org/api/mentions">mentioned</a>.
/// See <a href="https://corefork.telegram.org/constructor/inputMessagesFilterMyMentions" />
///</summary>
[TlObject(0xc1f8e69a)]
public sealed class TInputMessagesFilterMyMentions : IMessagesFilter
{
    public uint ConstructorId => 0xc1f8e69a;


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