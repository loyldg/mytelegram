﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// The specified bot commands will be valid only in a specific dialog.
/// See <a href="https://corefork.telegram.org/constructor/botCommandScopePeer" />
///</summary>
[TlObject(0xdb9d897d)]
public sealed class TBotCommandScopePeer : IBotCommandScope
{
    public uint ConstructorId => 0xdb9d897d;
    ///<summary>
    /// The dialog
    /// See <a href="https://corefork.telegram.org/type/InputPeer" />
    ///</summary>
    public MyTelegram.Schema.IInputPeer Peer { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Peer);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Peer = reader.Read<MyTelegram.Schema.IInputPeer>();
    }
}