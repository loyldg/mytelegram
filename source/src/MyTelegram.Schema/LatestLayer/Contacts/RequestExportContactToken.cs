﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Contacts;

///<summary>
/// Generates a <a href="https://corefork.telegram.org/api/links#temporary-profile-links">temporary profile link</a> for the currently logged-in user.
/// See <a href="https://corefork.telegram.org/method/contacts.exportContactToken" />
///</summary>
[TlObject(0xf8654027)]
public sealed class RequestExportContactToken : IRequest<MyTelegram.Schema.IExportedContactToken>
{
    public uint ConstructorId => 0xf8654027;

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);

    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {

    }
}
