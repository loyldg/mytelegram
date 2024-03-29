﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Suggested <a href="https://corefork.telegram.org/api/folders">folders</a>
/// See <a href="https://corefork.telegram.org/constructor/dialogFilterSuggested" />
///</summary>
[TlObject(0x77744d4a)]
public sealed class TDialogFilterSuggested : IDialogFilterSuggested
{
    public uint ConstructorId => 0x77744d4a;
    ///<summary>
    /// <a href="https://corefork.telegram.org/api/folders">Folder info</a>
    /// See <a href="https://corefork.telegram.org/type/DialogFilter" />
    ///</summary>
    public MyTelegram.Schema.IDialogFilter Filter { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/folders">Folder</a> description
    ///</summary>
    public string Description { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Filter);
        writer.Write(Description);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Filter = reader.Read<MyTelegram.Schema.IDialogFilter>();
        Description = reader.ReadString();
    }
}