﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Indicates the default notification sound should be used
/// See <a href="https://corefork.telegram.org/constructor/notificationSoundDefault" />
///</summary>
[TlObject(0x97e8bebe)]
public sealed class TNotificationSoundDefault : INotificationSound
{
    public uint ConstructorId => 0x97e8bebe;


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