﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;


///<summary>
///See <a href="https://core.telegram.org/constructor/inputPeerChannelFromMessage" />
///</summary>
[TlObject(0xbd2a0840)]
public sealed class TInputPeerChannelFromMessage : IInputPeer
{
    public uint ConstructorId => 0xbd2a0840;

    ///<summary>
    ///See <a href="https://core.telegram.org/type/InputPeer" />
    ///</summary>
    public MyTelegram.Schema.IInputPeer Peer { get; set; }
    public int MsgId { get; set; }
    public long ChannelId { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(BinaryWriter bw)
    {
        ComputeFlag();
        bw.Write(ConstructorId);
        Peer.Serialize(bw);
        bw.Write(MsgId);
        bw.Write(ChannelId);
    }

    public void Deserialize(BinaryReader br)
    {
        Peer = br.Deserialize<MyTelegram.Schema.IInputPeer>();
        MsgId = br.ReadInt32();
        ChannelId = br.ReadInt64();
    }
}