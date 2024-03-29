﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// View, forward counter + info about replies
/// See <a href="https://corefork.telegram.org/constructor/messages.messageViews" />
///</summary>
[TlObject(0xb6c4f543)]
public sealed class TMessageViews : IMessageViews
{
    public uint ConstructorId => 0xb6c4f543;
    ///<summary>
    /// View, forward counter + info about replies
    ///</summary>
    public TVector<MyTelegram.Schema.IMessageViews> Views { get; set; }

    ///<summary>
    /// Chats mentioned in constructor
    ///</summary>
    public TVector<MyTelegram.Schema.IChat> Chats { get; set; }

    ///<summary>
    /// Users mentioned in constructor
    ///</summary>
    public TVector<MyTelegram.Schema.IUser> Users { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Views);
        writer.Write(Chats);
        writer.Write(Users);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Views = reader.Read<TVector<MyTelegram.Schema.IMessageViews>>();
        Chats = reader.Read<TVector<MyTelegram.Schema.IChat>>();
        Users = reader.Read<TVector<MyTelegram.Schema.IUser>>();
    }
}