﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Deletes communication history.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 CHAT_REVOKE_DATE_UNSUPPORTED <code>min_date</code> and <code>max_date</code> are not available for using with non-user peers.
/// 400 MAX_DATE_INVALID The specified maximum date is invalid.
/// 400 MESSAGE_ID_INVALID The provided message id is invalid.
/// 400 MIN_DATE_INVALID The specified minimum date is invalid.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.deleteHistory" />
///</summary>
[TlObject(0xb08f922a)]
public sealed class RequestDeleteHistory : IRequest<MyTelegram.Schema.Messages.IAffectedHistory>
{
    public uint ConstructorId => 0xb08f922a;
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    public BitArray Flags { get; set; } = new BitArray(32);

    ///<summary>
    /// Just clear history for the current user, without actually removing messages for every chat user
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool JustClear { get; set; }

    ///<summary>
    /// Whether to delete the message history for all chat participants
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool Revoke { get; set; }

    ///<summary>
    /// User or chat, communication history of which will be deleted
    /// See <a href="https://corefork.telegram.org/type/InputPeer" />
    ///</summary>
    public MyTelegram.Schema.IInputPeer Peer { get; set; }

    ///<summary>
    /// Maximum ID of message to delete
    ///</summary>
    public int MaxId { get; set; }

    ///<summary>
    /// Delete all messages newer than this UNIX timestamp
    ///</summary>
    public int? MinDate { get; set; }

    ///<summary>
    /// Delete all messages older than this UNIX timestamp
    ///</summary>
    public int? MaxDate { get; set; }

    public void ComputeFlag()
    {
        if (JustClear) { Flags[0] = true; }
        if (Revoke) { Flags[1] = true; }
        if (/*MinDate != 0 && */MinDate.HasValue) { Flags[2] = true; }
        if (/*MaxDate != 0 && */MaxDate.HasValue) { Flags[3] = true; }
    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        writer.Write(Peer);
        writer.Write(MaxId);
        if (Flags[2]) { writer.Write(MinDate.Value); }
        if (Flags[3]) { writer.Write(MaxDate.Value); }
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        if (Flags[0]) { JustClear = true; }
        if (Flags[1]) { Revoke = true; }
        Peer = reader.Read<MyTelegram.Schema.IInputPeer>();
        MaxId = reader.ReadInt32();
        if (Flags[2]) { MinDate = reader.ReadInt32(); }
        if (Flags[3]) { MaxDate = reader.ReadInt32(); }
    }
}
