﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Photos;

///<summary>
/// Photo with auxiliary data.
/// See <a href="https://corefork.telegram.org/constructor/photos.photo" />
///</summary>
[TlObject(0x20212ca8)]
public sealed class TPhoto : IPhoto
{
    public uint ConstructorId => 0x20212ca8;
    ///<summary>
    /// Photo
    /// See <a href="https://corefork.telegram.org/type/Photo" />
    ///</summary>
    public MyTelegram.Schema.IPhoto Photo { get; set; }

    ///<summary>
    /// Users
    ///</summary>
    public TVector<MyTelegram.Schema.IUser> Users { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Photo);
        writer.Write(Users);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Photo = reader.Read<MyTelegram.Schema.IPhoto>();
        Users = reader.Read<TVector<MyTelegram.Schema.IUser>>();
    }
}