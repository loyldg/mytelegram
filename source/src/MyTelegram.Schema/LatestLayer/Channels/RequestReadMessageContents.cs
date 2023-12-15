﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Channels;

///<summary>
/// Mark <a href="https://corefork.telegram.org/api/channel">channel/supergroup</a> message contents as read
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// See <a href="https://corefork.telegram.org/method/channels.readMessageContents" />
///</summary>
[TlObject(0xeab5dc38)]
public sealed class RequestReadMessageContents : IRequest<IBool>
{
    public uint ConstructorId => 0xeab5dc38;
    ///<summary>
    /// <a href="https://corefork.telegram.org/api/channel">Channel/supergroup</a>
    /// See <a href="https://corefork.telegram.org/type/InputChannel" />
    ///</summary>
    public MyTelegram.Schema.IInputChannel Channel { get; set; }

    ///<summary>
    /// IDs of messages whose contents should be marked as read
    ///</summary>
    public TVector<int> Id { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Channel);
        writer.Write(Id);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Channel = reader.Read<MyTelegram.Schema.IInputChannel>();
        Id = reader.Read<TVector<int>>();
    }
}