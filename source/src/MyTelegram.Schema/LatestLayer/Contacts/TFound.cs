﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Contacts;

///<summary>
/// Users found by name substring and auxiliary data.
/// See <a href="https://corefork.telegram.org/constructor/contacts.found" />
///</summary>
[TlObject(0xb3134d9d)]
public sealed class TFound : IFound
{
    public uint ConstructorId => 0xb3134d9d;
    ///<summary>
    /// Personalized results
    ///</summary>
    public TVector<MyTelegram.Schema.IPeer> MyResults { get; set; }

    ///<summary>
    /// List of found user identifiers
    ///</summary>
    public TVector<MyTelegram.Schema.IPeer> Results { get; set; }

    ///<summary>
    /// Found chats
    ///</summary>
    public TVector<MyTelegram.Schema.IChat> Chats { get; set; }

    ///<summary>
    /// List of users
    ///</summary>
    public TVector<MyTelegram.Schema.IUser> Users { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(MyResults);
        writer.Write(Results);
        writer.Write(Chats);
        writer.Write(Users);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        MyResults = reader.Read<TVector<MyTelegram.Schema.IPeer>>();
        Results = reader.Read<TVector<MyTelegram.Schema.IPeer>>();
        Chats = reader.Read<TVector<MyTelegram.Schema.IChat>>();
        Users = reader.Read<TVector<MyTelegram.Schema.IUser>>();
    }
}