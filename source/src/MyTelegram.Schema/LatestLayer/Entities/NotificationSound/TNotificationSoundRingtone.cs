﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// A specific previously uploaded notification sound should be used
/// See <a href="https://corefork.telegram.org/constructor/notificationSoundRingtone" />
///</summary>
[TlObject(0xff6c8049)]
public sealed class TNotificationSoundRingtone : INotificationSound
{
    public uint ConstructorId => 0xff6c8049;
    ///<summary>
    /// Document ID of notification sound uploaded using <a href="https://corefork.telegram.org/method/account.uploadRingtone">account.uploadRingtone</a>
    ///</summary>
    public long Id { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Id);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Id = reader.ReadInt64();
    }
}