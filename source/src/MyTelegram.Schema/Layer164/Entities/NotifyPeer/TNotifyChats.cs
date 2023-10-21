﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Notifications generated by all groups.
/// See <a href="https://corefork.telegram.org/constructor/notifyChats" />
///</summary>
[TlObject(0xc007cec3)]
public sealed class TNotifyChats : INotifyPeer
{
    public uint ConstructorId => 0xc007cec3;


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