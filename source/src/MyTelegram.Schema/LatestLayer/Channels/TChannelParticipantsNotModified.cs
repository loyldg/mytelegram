﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Channels;

///<summary>
/// No new participant info could be found
/// See <a href="https://corefork.telegram.org/constructor/channels.channelParticipantsNotModified" />
///</summary>
[TlObject(0xf0173fe9)]
public sealed class TChannelParticipantsNotModified : IChannelParticipants
{
    public uint ConstructorId => 0xf0173fe9;


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