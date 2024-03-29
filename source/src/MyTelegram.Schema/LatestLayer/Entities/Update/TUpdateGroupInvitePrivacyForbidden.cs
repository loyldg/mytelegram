﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// 0-N updates of this type may be returned only when invoking <a href="https://corefork.telegram.org/method/messages.addChatUser">messages.addChatUser</a>, <a href="https://corefork.telegram.org/method/channels.inviteToChannel">channels.inviteToChannel</a> or <a href="https://corefork.telegram.org/method/messages.createChat">messages.createChat</a>: it indicates we couldn't add a user to a chat because of their privacy settings; if required, an <a href="https://corefork.telegram.org/api/invites">invite link</a> can be shared with the user, instead.
/// See <a href="https://corefork.telegram.org/constructor/updateGroupInvitePrivacyForbidden" />
///</summary>
[TlObject(0xccf08ad6)]
public sealed class TUpdateGroupInvitePrivacyForbidden : IUpdate
{
    public uint ConstructorId => 0xccf08ad6;
    ///<summary>
    /// ID of the user we couldn't add.
    ///</summary>
    public long UserId { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(UserId);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        UserId = reader.ReadInt64();
    }
}