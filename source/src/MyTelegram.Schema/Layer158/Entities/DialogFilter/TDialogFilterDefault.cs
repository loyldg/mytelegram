﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;


///<summary>
///See <a href="https://core.telegram.org/constructor/dialogFilterDefault" />
///</summary>
[TlObject(0x363293ae)]
public sealed class TDialogFilterDefault : IDialogFilter
{
    public uint ConstructorId => 0x363293ae;


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