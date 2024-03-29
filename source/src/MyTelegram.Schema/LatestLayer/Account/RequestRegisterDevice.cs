﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Account;

///<summary>
/// Register device to receive <a href="https://corefork.telegram.org/api/push-updates">PUSH notifications</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 TOKEN_EMPTY The specified token is empty.
/// 400 TOKEN_INVALID The provided token is invalid.
/// 400 TOKEN_TYPE_INVALID The specified token type is invalid.
/// 400 WEBPUSH_AUTH_INVALID The specified web push authentication secret is invalid.
/// 400 WEBPUSH_KEY_INVALID The specified web push elliptic curve Diffie-Hellman public key is invalid.
/// 400 WEBPUSH_TOKEN_INVALID The specified web push token is invalid.
/// See <a href="https://corefork.telegram.org/method/account.registerDevice" />
///</summary>
[TlObject(0xec86017a)]
public sealed class RequestRegisterDevice : IRequest<IBool>
{
    public uint ConstructorId => 0xec86017a;
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    public BitArray Flags { get; set; } = new BitArray(32);

    ///<summary>
    /// Avoid receiving (silent and invisible background) notifications. Useful to save battery.
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool NoMuted { get; set; }

    ///<summary>
    /// Device token type, see <a href="https://corefork.telegram.org/api/push-updates#subscribing-to-notifications">PUSH updates</a> for the possible values.
    ///</summary>
    public int TokenType { get; set; }

    ///<summary>
    /// Device token, see <a href="https://corefork.telegram.org/api/push-updates#subscribing-to-notifications">PUSH updates</a> for the possible values.
    ///</summary>
    public string Token { get; set; }

    ///<summary>
    /// If <a href="https://corefork.telegram.org/constructor/boolTrue">(boolTrue)</a> is transmitted, a sandbox-certificate will be used during transmission.
    /// See <a href="https://corefork.telegram.org/type/Bool" />
    ///</summary>
    public bool AppSandbox { get; set; }

    ///<summary>
    /// For FCM and APNS VoIP, optional encryption key used to encrypt push notifications
    ///</summary>
    public byte[] Secret { get; set; }

    ///<summary>
    /// List of user identifiers of other users currently using the client
    ///</summary>
    public TVector<long> OtherUids { get; set; }

    public void ComputeFlag()
    {
        if (NoMuted) { Flags[0] = true; }

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        writer.Write(TokenType);
        writer.Write(Token);
        writer.Write(AppSandbox);
        writer.Write(Secret);
        writer.Write(OtherUids);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        if (Flags[0]) { NoMuted = true; }
        TokenType = reader.ReadInt32();
        Token = reader.ReadString();
        AppSandbox = reader.Read();
        Secret = reader.ReadBytes();
        OtherUids = reader.Read<TVector<long>>();
    }
}
