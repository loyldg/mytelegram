﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Auth;

///<summary>
/// Resend the login code via another medium, the phone code type is determined by the return value of the previous auth.sendCode/auth.resendCode: see <a href="https://corefork.telegram.org/api/auth">login</a> for more info.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 PHONE_CODE_EMPTY phone_code is missing.
/// 400 PHONE_CODE_EXPIRED The phone code you provided has expired.
/// 400 PHONE_CODE_HASH_EMPTY phone_code_hash is missing.
/// 406 PHONE_NUMBER_INVALID The phone number is invalid.
/// 406 SEND_CODE_UNAVAILABLE Returned when all available options for this type of number were already used (e.g. flash-call, then SMS, then this error might be returned to trigger a second resend).
/// See <a href="https://corefork.telegram.org/method/auth.resendCode" />
///</summary>
[TlObject(0x3ef1a9bf)]
public sealed class RequestResendCode : IRequest<MyTelegram.Schema.Auth.ISentCode>
{
    public uint ConstructorId => 0x3ef1a9bf;
    ///<summary>
    /// The phone number
    ///</summary>
    public string PhoneNumber { get; set; }

    ///<summary>
    /// The phone code hash obtained from <a href="https://corefork.telegram.org/method/auth.sendCode">auth.sendCode</a>
    ///</summary>
    public string PhoneCodeHash { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(PhoneNumber);
        writer.Write(PhoneCodeHash);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        PhoneNumber = reader.ReadString();
        PhoneCodeHash = reader.ReadString();
    }
}