﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Contacts;

///<summary>
///See <a href="https://core.telegram.org/method/contacts.exportContactToken" />
///</summary>
[TlObject(0xf8654027)]
public sealed class RequestExportContactToken : IRequest<MyTelegram.Schema.IExportedContactToken>
{
    public uint ConstructorId => 0xf8654027;

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