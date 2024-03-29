﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Phone;

///<summary>
/// Start a telegram phone call
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CALL_PROTOCOL_FLAGS_INVALID Call protocol flags invalid.
/// 400 INPUT_USER_DEACTIVATED The specified user was deleted.
/// 400 PARTICIPANT_VERSION_OUTDATED The other participant does not use an up to date telegram client with support for calls.
/// 400 USER_ID_INVALID The provided user ID is invalid.
/// 403 USER_IS_BLOCKED You were blocked by this user.
/// 403 USER_PRIVACY_RESTRICTED The user's privacy settings do not allow you to do this.
/// See <a href="https://corefork.telegram.org/method/phone.requestCall" />
///</summary>
[TlObject(0x42ff96ed)]
public sealed class RequestRequestCall : IRequest<MyTelegram.Schema.Phone.IPhoneCall>
{
    public uint ConstructorId => 0x42ff96ed;
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    public BitArray Flags { get; set; } = new BitArray(32);

    ///<summary>
    /// Whether to start a video call
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool Video { get; set; }

    ///<summary>
    /// Destination of the phone call
    /// See <a href="https://corefork.telegram.org/type/InputUser" />
    ///</summary>
    public MyTelegram.Schema.IInputUser UserId { get; set; }

    ///<summary>
    /// Random ID to avoid resending the same object
    ///</summary>
    public int RandomId { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/end-to-end/voice-calls">Parameter for E2E encryption key exchange »</a>
    ///</summary>
    public byte[] GAHash { get; set; }

    ///<summary>
    /// Phone call settings
    /// See <a href="https://corefork.telegram.org/type/PhoneCallProtocol" />
    ///</summary>
    public MyTelegram.Schema.IPhoneCallProtocol Protocol { get; set; }

    public void ComputeFlag()
    {
        if (Video) { Flags[0] = true; }

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        writer.Write(UserId);
        writer.Write(RandomId);
        writer.Write(GAHash);
        writer.Write(Protocol);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        if (Flags[0]) { Video = true; }
        UserId = reader.Read<MyTelegram.Schema.IInputUser>();
        RandomId = reader.ReadInt32();
        GAHash = reader.ReadBytes();
        Protocol = reader.Read<MyTelegram.Schema.IPhoneCallProtocol>();
    }
}
