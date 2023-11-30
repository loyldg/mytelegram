﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Pin/unpin a dialog
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 PEER_HISTORY_EMPTY You can't pin an empty chat with a user.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 PINNED_DIALOGS_TOO_MUCH Too many pinned dialogs.
/// See <a href="https://corefork.telegram.org/method/messages.toggleDialogPin" />
///</summary>
[TlObject(0xa731e257)]
public sealed class RequestToggleDialogPin : IRequest<IBool>
{
    public uint ConstructorId => 0xa731e257;
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    public BitArray Flags { get; set; } = new BitArray(32);

    ///<summary>
    /// Whether to pin or unpin the dialog
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool Pinned { get; set; }

    ///<summary>
    /// The dialog to pin
    /// See <a href="https://corefork.telegram.org/type/InputDialogPeer" />
    ///</summary>
    public MyTelegram.Schema.IInputDialogPeer Peer { get; set; }

    public void ComputeFlag()
    {
        if (Pinned) { Flags[0] = true; }

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        writer.Write(Peer);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        if (Flags[0]) { Pinned = true; }
        Peer = reader.Read<MyTelegram.Schema.IInputDialogPeer>();
    }
}