﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// A message was posted in a channel
/// See <a href="https://corefork.telegram.org/constructor/channelAdminLogEventActionSendMessage" />
///</summary>
[TlObject(0x278f2868)]
public sealed class TChannelAdminLogEventActionSendMessage : IChannelAdminLogEventAction
{
    public uint ConstructorId => 0x278f2868;
    ///<summary>
    /// The message that was sent
    /// See <a href="https://corefork.telegram.org/type/Message" />
    ///</summary>
    public MyTelegram.Schema.IMessage Message { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Message);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Message = reader.Read<MyTelegram.Schema.IMessage>();
    }
}