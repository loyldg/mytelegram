﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Account;

///<summary>
///See <a href="https://core.telegram.org/method/account.getNotifySettings" />
///</summary>
[TlObject(0x12b3ad31)]
public sealed class RequestGetNotifySettings : IRequest<MyTelegram.Schema.IPeerNotifySettings>
{
    public uint ConstructorId => 0x12b3ad31;

    ///<summary>
    ///See <a href="https://core.telegram.org/type/InputNotifyPeer" />
    ///</summary>
    public MyTelegram.Schema.IInputNotifyPeer Peer { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(BinaryWriter bw)
    {
        ComputeFlag();
        bw.Write(ConstructorId);
        Peer.Serialize(bw);
    }

    public void Deserialize(BinaryReader br)
    {
        Peer = br.Deserialize<MyTelegram.Schema.IInputNotifyPeer>();
    }
}