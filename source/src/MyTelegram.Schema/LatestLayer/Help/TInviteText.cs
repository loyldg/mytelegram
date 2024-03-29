﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Help;

///<summary>
/// Text of a text message with an invitation to install Telegram.
/// See <a href="https://corefork.telegram.org/constructor/help.inviteText" />
///</summary>
[TlObject(0x18cb9f78)]
public sealed class TInviteText : IInviteText
{
    public uint ConstructorId => 0x18cb9f78;
    ///<summary>
    /// Text of the message
    ///</summary>
    public string Message { get; set; }

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
        Message = reader.ReadString();
    }
}