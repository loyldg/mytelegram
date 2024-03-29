﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Sends a current user typing event (see <a href="https://corefork.telegram.org/type/SendMessageAction">SendMessageAction</a> for all event types) to a conversation partner or group.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 403 GROUPCALL_FORBIDDEN The group call has already ended.
/// 400 INPUT_USER_DEACTIVATED The specified user was deleted.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 USER_BANNED_IN_CHANNEL You're banned from sending messages in supergroups/channels.
/// 403 USER_IS_BLOCKED You were blocked by this user.
/// 400 USER_IS_BOT Bots can't send messages to other bots.
/// See <a href="https://corefork.telegram.org/method/messages.setTyping" />
///</summary>
[TlObject(0x58943ee2)]
public sealed class RequestSetTyping : IRequest<IBool>
{
    public uint ConstructorId => 0x58943ee2;
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    public BitArray Flags { get; set; } = new BitArray(32);

    ///<summary>
    /// Target user or group
    /// See <a href="https://corefork.telegram.org/type/InputPeer" />
    ///</summary>
    public MyTelegram.Schema.IInputPeer Peer { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/threads">Topic ID</a>
    ///</summary>
    public int? TopMsgId { get; set; }

    ///<summary>
    /// Type of action
    /// See <a href="https://corefork.telegram.org/type/SendMessageAction" />
    ///</summary>
    public MyTelegram.Schema.ISendMessageAction Action { get; set; }

    public void ComputeFlag()
    {
        if (/*TopMsgId != 0 && */TopMsgId.HasValue) { Flags[0] = true; }

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        writer.Write(Peer);
        if (Flags[0]) { writer.Write(TopMsgId.Value); }
        writer.Write(Action);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        Peer = reader.Read<MyTelegram.Schema.IInputPeer>();
        if (Flags[0]) { TopMsgId = reader.ReadInt32(); }
        Action = reader.Read<MyTelegram.Schema.ISendMessageAction>();
    }
}
