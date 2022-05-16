﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
///See <a href="https://core.telegram.org/method/messages.setChatAvailableReactions" />
///</summary>
[TlObject(0x14050ea6)]
public sealed class RequestSetChatAvailableReactions : IRequest<MyTelegram.Schema.IUpdates>
{
    public uint ConstructorId => 0x14050ea6;

    ///<summary>
    ///See <a href="https://core.telegram.org/type/InputPeer" />
    ///</summary>
    public MyTelegram.Schema.IInputPeer Peer { get; set; }
    public TVector<string> AvailableReactions { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(BinaryWriter bw)
    {
        ComputeFlag();
        bw.Write(ConstructorId);
        Peer.Serialize(bw);
        AvailableReactions.Serialize(bw);
    }

    public void Deserialize(BinaryReader br)
    {
        Peer = br.Deserialize<MyTelegram.Schema.IInputPeer>();
        AvailableReactions = br.Deserialize<TVector<string>>();
    }
}