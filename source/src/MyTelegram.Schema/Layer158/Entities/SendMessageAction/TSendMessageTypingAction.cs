﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;


///<summary>
///See <a href="https://core.telegram.org/constructor/sendMessageTypingAction" />
///</summary>
[TlObject(0x16bf744e)]
public sealed class TSendMessageTypingAction : ISendMessageAction
{
    public uint ConstructorId => 0x16bf744e;


    public void ComputeFlag()
    {

    }

    public void Serialize(BinaryWriter bw)
    {
        ComputeFlag();
        bw.Write(ConstructorId);

    }

    public void Deserialize(BinaryReader br)
    {

    }
}