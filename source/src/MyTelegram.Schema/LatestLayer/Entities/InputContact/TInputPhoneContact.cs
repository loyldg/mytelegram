﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Phone contact.
/// See <a href="https://corefork.telegram.org/constructor/inputPhoneContact" />
///</summary>
[TlObject(0xf392b7f4)]
public sealed class TInputPhoneContact : IInputContact
{
    public uint ConstructorId => 0xf392b7f4;
    ///<summary>
    /// An arbitrary 64-bit integer: it should be set, for example, to an incremental number when using <a href="https://corefork.telegram.org/method/contacts.importContacts">contacts.importContacts</a>, in order to retry importing only the contacts that weren't imported successfully, according to the client_ids returned in <a href="https://corefork.telegram.org/constructor/contacts.importedContacts">contacts.importedContacts</a>.<code>retry_contacts</code>.
    ///</summary>
    public long ClientId { get; set; }

    ///<summary>
    /// Phone number
    ///</summary>
    public string Phone { get; set; }

    ///<summary>
    /// Contact's first name
    ///</summary>
    public string FirstName { get; set; }

    ///<summary>
    /// Contact's last name
    ///</summary>
    public string LastName { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(ClientId);
        writer.Write(Phone);
        writer.Write(FirstName);
        writer.Write(LastName);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        ClientId = reader.ReadInt64();
        Phone = reader.ReadString();
        FirstName = reader.ReadString();
        LastName = reader.ReadString();
    }
}