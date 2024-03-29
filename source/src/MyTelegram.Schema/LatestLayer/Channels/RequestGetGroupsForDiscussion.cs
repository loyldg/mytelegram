﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Channels;

///<summary>
/// Get all groups that can be used as <a href="https://corefork.telegram.org/api/discussion">discussion groups</a>.Returned <a href="https://corefork.telegram.org/api/channel#basic-groups">basic group chats</a> must be first upgraded to <a href="https://corefork.telegram.org/api/channel#supergroups">supergroups</a> before they can be set as a discussion group.<br>
/// To set a returned supergroup as a discussion group, access to its old messages must be enabled using <a href="https://corefork.telegram.org/method/channels.togglePreHistoryHidden">channels.togglePreHistoryHidden</a>, first.
/// See <a href="https://corefork.telegram.org/method/channels.getGroupsForDiscussion" />
///</summary>
[TlObject(0xf5dad378)]
public sealed class RequestGetGroupsForDiscussion : IRequest<MyTelegram.Schema.Messages.IChats>
{
    public uint ConstructorId => 0xf5dad378;

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
