﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Get full info about a <a href="https://corefork.telegram.org/api/channel#basic-groups">basic group</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.getFullChat" />
///</summary>
[TlObject(0xaeb00b34)]
public sealed class RequestGetFullChat : IRequest<MyTelegram.Schema.Messages.IChatFull>
{
    public uint ConstructorId => 0xaeb00b34;
    ///<summary>
    /// <a href="https://corefork.telegram.org/api/channel#basic-groups">Basic group</a> ID.
    ///</summary>
    public long ChatId { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(ChatId);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        ChatId = reader.ReadInt64();
    }
}
