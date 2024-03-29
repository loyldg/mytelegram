﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Secure <a href="https://corefork.telegram.org/passport">passport</a> file, for more info <a href="https://corefork.telegram.org/passport/encryption#inputsecurefile">see the passport docs »</a>
/// See <a href="https://corefork.telegram.org/constructor/secureFile" />
///</summary>
[TlObject(0x7d09c27e)]
public sealed class TSecureFile : ISecureFile
{
    public uint ConstructorId => 0x7d09c27e;
    ///<summary>
    /// ID
    ///</summary>
    public long Id { get; set; }

    ///<summary>
    /// Access hash
    ///</summary>
    public long AccessHash { get; set; }

    ///<summary>
    /// File size
    ///</summary>
    public long Size { get; set; }

    ///<summary>
    /// DC ID
    ///</summary>
    public int DcId { get; set; }

    ///<summary>
    /// Date of upload
    ///</summary>
    public int Date { get; set; }

    ///<summary>
    /// File hash
    ///</summary>
    public byte[] FileHash { get; set; }

    ///<summary>
    /// Secret
    ///</summary>
    public byte[] Secret { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Id);
        writer.Write(AccessHash);
        writer.Write(Size);
        writer.Write(DcId);
        writer.Write(Date);
        writer.Write(FileHash);
        writer.Write(Secret);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Id = reader.ReadInt64();
        AccessHash = reader.ReadInt64();
        Size = reader.ReadInt64();
        DcId = reader.ReadInt32();
        Date = reader.ReadInt32();
        FileHash = reader.ReadBytes();
        Secret = reader.ReadBytes();
    }
}