namespace MyTelegram.MTProto.Tests;

public class AesHelperTest : TestsFor<AesHelper>
{
    [Theory]
    [InlineData(@"52 BF FD 82 77 DF 92 00 DE 7B DB F7 A3 05 1E AE
D0 83 8A 45 D2 20 ED DB 5D EC 68 6A BD FC CA AB",
        @"40 EF 69 0A 99 B9 33 56 D1 62 77 F0 40 5A 1D 8E",
        @"00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00",
        @"0",
        //        @"00 00 00 00 00 00 00 00",
        //        @"01 00 00 00 CE FD 3C 64",
        //        @"40 00 00 00",
        //        @"63 24 16 05 95 D8 D3 0A 76 09 85 17 57 C1 7B 1F
        //13 D0 B9 0B 68 2D 71 9D 71 BF 95 55 2A EE 76 1E
        //CD E2 5B 32 08 17 ED 48 94 1A 08 F9 81 00 00 00
        //15 C4 B5 1C 01 00 00 00 A4 BD 15 12 08 F5 27 CE",
        @"15 00 00 00 00 00 00 00 00 01 00 00 00 CE FD 3C
            64 40 00 00 00 63 24 16 05 95 D8 D3 0A 76 09 85
    17 57 C1 7B 1F 13 D0 B9 0B 68 2D 71 9D 71 BF 95
    55 2A EE 76 1E CD E2 5B 32 08 17 ED 48 94 1A 08
    F9 81 00 00 00 15 C4 B5 1C 01 00 00 00 A4 BD 15
    12 08 F5 27 CE",
//        @"52 BF FD 82 77 DF 92 00 DE 7B DB F7 A3 05 1E AE
//D0 83 8A 45 D2 20 ED DB 5D EC 68 6A BD FC CA AB",
        @"40 EF 69 0A 99 B9 33 56 D1 62 77 F0 40 5A 1D 94",
        @"2D 28 86 16 B1 BE 35 07 34 D2 56 5E 57 59 02 AB",
        @"5",
        @"E6 8A 83 90 46 3B EC 5D DB C8 E4 29 C3 E9 36 26
AE 58 32 96 D6 2C 57 B6 E7 AE 60 93 0E F4 72 1A
6D 40 62 DA 49 74 83 CE 5A 71 89 E5 0B 79 A1 46
80 FA 28 DD BE 30 7E 3D 3E A1 3A FF D0 2E FD 67
22 D8 90 0F 3D E7 EA 74 0A 8F C3 C1 E3 D0 5C AF
3F 20 73 31 7F"
    )]
    public void Ctr128Test(string receiveKey,
        string receiveIv,
        string ecounter,
        string n,
        //string authKeyId,
        //string messageId,
        //string messageDataLength,
        //string messageData,
        string buffer,
        //string receiveKey2,
        string receiveIv2,
        string ecounter2,
        string n2,
        string expectedData)
    {
        var span = buffer.ToBytes();
        var s = new CtrState
        {
            ECounter=ecounter.ToBytes(),
            Iv=receiveIv.ToBytes(),
            Number=int.Parse(n)
        };
        var expectedBytes=expectedData.ToBytes();
        //var key = receiveKey.ToBytes();

        Sut.Ctr128Encrypt(span,receiveKey.ToBytes(),s);
        //var d2 = new byte[span.Length];
        //Sut.Ctr128Encrypt(span,d2,key,s);

        expectedBytes.ShouldBeEquivalentTo(span);
        n2.ShouldBe(s.Number.ToString());
        receiveIv2.ToBytes().ShouldBeEquivalentTo(s.Iv);
        ecounter2.ToBytes().ShouldBeEquivalentTo(s.ECounter);
    }
}