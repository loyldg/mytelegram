﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// The hidden prehistory setting was <a href="https://corefork.telegram.org/method/channels.togglePreHistoryHidden">changed</a>
/// See <a href="https://corefork.telegram.org/constructor/channelAdminLogEventActionTogglePreHistoryHidden" />
///</summary>
[TlObject(0x5f5c95f1)]
public sealed class TChannelAdminLogEventActionTogglePreHistoryHidden : IChannelAdminLogEventAction
{
    public uint ConstructorId => 0x5f5c95f1;
    ///<summary>
    /// New value
    /// See <a href="https://corefork.telegram.org/type/Bool" />
    ///</summary>
    public bool NewValue { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(NewValue);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        NewValue = reader.Read();
    }
}