﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Account;

///<summary>
///See <a href="https://core.telegram.org/method/account.uploadRingtone" />
///</summary>
[TlObject(0x831a83a2)]
public sealed class RequestUploadRingtone : IRequest<MyTelegram.Schema.IDocument>
{
    public uint ConstructorId => 0x831a83a2;

    ///<summary>
    ///See <a href="https://core.telegram.org/type/InputFile" />
    ///</summary>
    public MyTelegram.Schema.IInputFile File { get; set; }
    public string FileName { get; set; }
    public string MimeType { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(BinaryWriter bw)
    {
        ComputeFlag();
        bw.Write(ConstructorId);
        File.Serialize(bw);
        bw.Serialize(FileName);
        bw.Serialize(MimeType);
    }

    public void Deserialize(BinaryReader br)
    {
        File = br.Deserialize<MyTelegram.Schema.IInputFile>();
        FileName = br.Deserialize<string>();
        MimeType = br.Deserialize<string>();
    }
}
