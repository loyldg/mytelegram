namespace MyTelegram.MTProto.Tests;

public class MtpMessageEncoderTest : TestsFor<MtpMessageEncoder>
{
    private readonly Mock<IMessageIdHelper> _messageIdHelperMock;
    public MtpMessageEncoderTest()
    {
        _messageIdHelperMock = new();
        Inject<IAesHelper>(new AesHelper());
        Inject(_messageIdHelperMock.Object);
    }

    [Theory]
    [InlineData(@"52 BF FD 82 77 DF 92 00 DE 7B DB F7 A3 05 1E AE
D0 83 8A 45 D2 20 ED DB 5D EC 68 6A BD FC CA AB",
        @"40 EF 69 0A 99 B9 33 56 D1 62 77 F0 40 5A 1D 8E",
        @"00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00",
        @"0",
        //@"00 00 00 00 00 00 00 00",
        @"01 00 00 00 CE FD 3C 64",
        //@"40 00 00 00",
        @"63 24 16 05 95 D8 D3 0A 76 09 85 17 57 C1 7B 1F
13 D0 B9 0B 68 2D 71 9D 71 BF 95 55 2A EE 76 1E
CD E2 5B 32 08 17 ED 48 94 1A 08 F9 81 00 00 00
15 C4 B5 1C 01 00 00 00 A4 BD 15 12 08 F5 27 CE",
//        @"15 00 00 00 00 00 00 00 00 01 00 00 00 CE FD 3C
//            64 40 00 00 00 63 24 16 05 95 D8 D3 0A 76 09 85
//    17 57 C1 7B 1F 13 D0 B9 0B 68 2D 71 9D 71 BF 95
//    55 2A EE 76 1E CD E2 5B 32 08 17 ED 48 94 1A 08
//    F9 81 00 00 00 15 C4 B5 1C 01 00 00 00 A4 BD 15
//    12 08 F5 27 CE",
//        @"52 BF FD 82 77 DF 92 00 DE 7B DB F7 A3 05 1E AE
//D0 83 8A 45 D2 20 ED DB 5D EC 68 6A BD FC CA AB",
//        @"40 EF 69 0A 99 B9 33 56 D1 62 77 F0 40 5A 1D 94",
//        @"2D 28 86 16 B1 BE 35 07 34 D2 56 5E 57 59 02 AB",
//        @"5",
        @"E6 8A 83 90 46 3B EC 5D DB C8 E4 29 C3 E9 36 26
AE 58 32 96 D6 2C 57 B6 E7 AE 60 93 0E F4 72 1A
6D 40 62 DA 49 74 83 CE 5A 71 89 E5 0B 79 A1 46
80 FA 28 DD BE 30 7E 3D 3E A1 3A FF D0 2E FD 67
22 D8 90 0F 3D E7 EA 74 0A 8F C3 C1 E3 D0 5C AF
3F 20 73 31 7F"
    )]

    [InlineData(
        @"52 BF FD 82 77 DF 92 00 DE 7B DB F7 A3 05 1E AE
D0 83 8A 45 D2 20 ED DB 5D EC 68 6A BD FC CA AB",
        @"40 EF 69 0A 99 B9 33 56 D1 62 77 F0 40 5A 1D 94",
        @"2D 28 86 16 B1 BE 35 07 34 D2 56 5E 57 59 02 AB",
        @"5",
        //@"00 00 00 00 00 00 00 00",
        @"05 00 00 00 CE FD 3C 64",
        //@"40 00 00 00",
        @"63 24 16 05 EC B2 AF A8 32 F0 2B 6B 7F E0 13 CE
2F 2E D9 72 CE 27 7C 4C AA 7B 8C 9E 89 4B 7D 82
51 4A BF E0 08 17 ED 48 94 1A 08 F9 81 00 00 00
15 C4 B5 1C 01 00 00 00 A4 BD 15 12 08 F5 27 CE",
//        @"15 00 00 00 00 00 00 00 00 05 00 00 00 CE FD 3C
//            64 40 00 00 00 63 24 16 05 EC B2 AF A8 32 F0 2B
//    6B 7F E0 13 CE 2F 2E D9 72 CE 27 7C 4C AA 7B 8C
//    9E 89 4B 7D 82 51 4A BF E0 08 17 ED 48 94 1A 08
//    F9 81 00 00 00 15 C4 B5 1C 01 00 00 00 A4 BD 15
//    12 08 F5 27 CE",
//        @"52 BF FD 82 77 DF 92 00 DE 7B DB F7 A3 05 1E AE
//D0 83 8A 45 D2 20 ED DB 5D EC 68 6A BD FC CA AB",
//        @"40 EF 69 0A 99 B9 33 56 D1 62 77 F0 40 5A 1D 99",
//        @"C8 4F 4E 7A 61 03 92 1A 4B FB BB D0 EC A8 23 CD",
//        @"10",
        @"AB 35 07 34 D2 56 5E 57 59 07 AB AE 60 FF 1A C9
10 C3 13 29 B7 A6 CF 32 B1 59 B8 CC 1C 5D 69 89
C7 31 EF F1 64 ED C4 B2 A4 C9 35 D5 E1 A7 64 4E
CB 34 11 7C B5 AB 30 0D 3D 58 1C E2 6C AE 95 16
5A 79 F5 F2 0A C3 A7 7B 11 E7 4B C8 4F EA C7 74
11 9A EF 6C 35"
    )]
    public void EncodeTest(
        string receiveKey,
        string receiveIv,
        string ecounter,
        string n,
        //string authKeyId,
        string messageId,
        //string messageDataLength,
        string messageData,
        //string buffer,
        //string receiveKey2,
        //string receiveIv2,
        //string ecounter2,
        //string n2,
        string expectedData)
    {
        var d = new ClientData();
        d.MtProtoType = ProtocolType.Abridge;
        d.ReceiveKey = receiveKey.ToBytes();
        d.ReceiveCtrState = new CtrState
        {
            ECounter = ecounter.ToBytes(),
            Iv = receiveIv.ToBytes(),
            Number = int.Parse(n)
        };
        var data = messageData.ToBytes();
        var m = new UnencryptedMessageResponse(0, data, string.Empty, 0);
        var encodedBytes = ArrayPool<byte>.Shared.Rent(data.Length + 21);
        Span<byte> span = encodedBytes;
        var messageId2 = BitConverter.ToInt64(messageId.ToBytes());
        _messageIdHelperMock.Setup(p => p.GenerateMessageId())
            .Returns(messageId2);
        var expectedBuffer = expectedData.ToBytes();

        var count = Sut.Encode(d, m, span);

        span.Slice(0, count).ToArray().ShouldBeEquivalentTo(expectedBuffer);
        //receiveKey2.ToBytes().ShouldBeEquivalentTo(d.ReceiveKey);
        //var iv2 = receiveIv2.ToBytes();
        //receiveIv2.ToBytes().ShouldBeEquivalentTo(d.ReceiveCtrState.Iv);
        //ecounter2.ToBytes().ShouldBeEquivalentTo(d.ReceiveCtrState.ECounter);
        //n2.ShouldBe(d.ReceiveCtrState.Number.ToString());
    }
}
