﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Bots;

///<summary>
///See <a href="https://core.telegram.org/method/bots.getBotMenuButton" />
///</summary>
[TlObject(0x9c60eb28)]
public sealed class RequestGetBotMenuButton : IRequest<MyTelegram.Schema.IBotMenuButton>
{
    public uint ConstructorId => 0x9c60eb28;

    ///<summary>
    ///See <a href="https://core.telegram.org/type/InputUser" />
    ///</summary>
    public MyTelegram.Schema.IInputUser UserId { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(BinaryWriter bw)
    {
        ComputeFlag();
        bw.Write(ConstructorId);
        UserId.Serialize(bw);
    }

    public void Deserialize(BinaryReader br)
    {
        UserId = br.Deserialize<MyTelegram.Schema.IInputUser>();
    }
}
