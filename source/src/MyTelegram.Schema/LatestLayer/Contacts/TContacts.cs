﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Contacts;

///<summary>
/// The current user's contact list and info on users.
/// See <a href="https://corefork.telegram.org/constructor/contacts.contacts" />
///</summary>
[TlObject(0xeae87e42)]
public sealed class TContacts : IContacts
{
    public uint ConstructorId => 0xeae87e42;
    ///<summary>
    /// Contact list
    ///</summary>
    public TVector<MyTelegram.Schema.IContact> Contacts { get; set; }

    ///<summary>
    /// Number of contacts that were saved successfully
    ///</summary>
    public int SavedCount { get; set; }

    ///<summary>
    /// User list
    ///</summary>
    public TVector<MyTelegram.Schema.IUser> Users { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Contacts);
        writer.Write(SavedCount);
        writer.Write(Users);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Contacts = reader.Read<TVector<MyTelegram.Schema.IContact>>();
        SavedCount = reader.ReadInt32();
        Users = reader.Read<TVector<MyTelegram.Schema.IUser>>();
    }
}