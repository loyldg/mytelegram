﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Contacts;

///<summary>
/// Returns the list of contact statuses.
/// See <a href="https://corefork.telegram.org/method/contacts.getStatuses" />
///</summary>
[TlObject(0xc4a353ee)]
public sealed class RequestGetStatuses : IRequest<TVector<MyTelegram.Schema.IContactStatus>>
{
    public uint ConstructorId => 0xc4a353ee;

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