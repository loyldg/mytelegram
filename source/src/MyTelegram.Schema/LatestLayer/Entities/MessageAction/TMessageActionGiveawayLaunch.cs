﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// A <a href="https://corefork.telegram.org/api/giveaways">giveaway</a> was started.
/// See <a href="https://corefork.telegram.org/constructor/messageActionGiveawayLaunch" />
///</summary>
[TlObject(0x332ba9ed)]
public sealed class TMessageActionGiveawayLaunch : IMessageAction
{
    public uint ConstructorId => 0x332ba9ed;


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