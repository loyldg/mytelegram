﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Contacts;

///<summary>
///See <a href="https://core.telegram.org/method/contacts.block" />
///</summary>
[TlObject(0x68cc1411)]
public sealed class RequestBlock : IRequest<IBool>
{
    public uint ConstructorId => 0x68cc1411;

    ///<summary>
    ///See <a href="https://core.telegram.org/type/InputPeer" />
    ///</summary>
    public MyTelegram.Schema.IInputPeer Id { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(BinaryWriter bw)
    {
        ComputeFlag();
        bw.Write(ConstructorId);
        Id.Serialize(bw);
    }

    public void Deserialize(BinaryReader br)
    {
        Id = br.Deserialize<MyTelegram.Schema.IInputPeer>();
    }
}