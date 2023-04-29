﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Chatlists;

///<summary>
///See <a href="https://core.telegram.org/method/chatlists.deleteExportedInvite" />
///</summary>
[TlObject(0x719c5c5e)]
public sealed class RequestDeleteExportedInvite : IRequest<IBool>
{
    public uint ConstructorId => 0x719c5c5e;

    ///<summary>
    ///See <a href="https://core.telegram.org/type/InputChatlist" />
    ///</summary>
    public MyTelegram.Schema.IInputChatlist Chatlist { get; set; }
    public string Slug { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(BinaryWriter bw)
    {
        ComputeFlag();
        bw.Write(ConstructorId);
        Chatlist.Serialize(bw);
        bw.Serialize(Slug);
    }

    public void Deserialize(BinaryReader br)
    {
        Chatlist = br.Deserialize<MyTelegram.Schema.IInputChatlist>();
        Slug = br.Deserialize<string>();
    }
}