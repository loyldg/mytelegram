﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Return only chat photo changes
/// See <a href="https://corefork.telegram.org/constructor/inputMessagesFilterChatPhotos" />
///</summary>
[TlObject(0x3a20ecb8)]
public sealed class TInputMessagesFilterChatPhotos : IMessagesFilter
{
    public uint ConstructorId => 0x3a20ecb8;


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