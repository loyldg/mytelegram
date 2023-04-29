using Microsoft.Extensions.Logging;

namespace MyTelegram.MTProto.Tests;

public class FirstPacketParserTest : TestsFor<FirstPacketParser>
{
    private readonly Mock<ILogger<FirstPacketParser>> _loggerMock;
    public FirstPacketParserTest()
    {
        _loggerMock = new();
        Inject<IAesHelper>(new AesHelper());
        Inject(_loggerMock.Object);
    }

    [Fact]
    public void ParseFirstPacket()
    {
        var nonce = @"E2 B5 09 29 B6 1A 48 C4 28 5C D7 02 01 A3 98 E5 C3 1E 81 A9 0C 34 B9 7A 0F 1C BC E9 0B 41 75 C9
9B 25 73 E7 36 4A 14 71 EF D6 77 7D 73 B2 45 5A 5E 3E BF 31 65 04 71 07 70 F7 7F 86 6A 9E 48 BC".ToBytes();
        var sendKey = @"28 5C D7 02 01 A3 98 E5 C3 1E 81 A9 0C 34 B9 7A 0F 1C BC E9 0B 41 75 C9 9B 25 73 E7 36 4A 14 71"
            .ToBytes();
        var sendIv = "EF D6 77 7D 73 B2 45 5A 5E 3E BF 31 65 04 71 0B".ToBytes();
        var receiveKey =
            @"07 71 04 65 31 BF 3E 5E 5A 45 B2 73 7D 77 D6 EF 71 14 4A 36 E7 73 25 9B C9 75 41 0B E9 BC 1C 0F"
                .ToBytes();
        var receiveIv = "7A B9 34 0C A9 81 1E C3 E5 98 A3 01 02 D7 5C 28".ToBytes();

        var d = Sut.Parse(nonce);

        d.SendKey.ShouldBeEquivalentTo(sendKey);
        d.SendState.Iv.ShouldBeEquivalentTo(sendIv);
        d.ReceiveKey.ShouldBeEquivalentTo(receiveKey);
        d.ReceiveState.Iv.ShouldBeEquivalentTo(receiveIv);
    }
}
