using Moq;
using MyTelegram.MessengerServer.Services.IdGenerator;
using MyTelegram.TestBase;

namespace MyTelegram.MessengerServer.Tests.Services.IdGenerator;

public class DefaultIdGeneratorTests : TestsFor<DefaultIdGenerator>
{
    private readonly Mock<IHiLoHighValueGenerator> _hiLoHighValueGenerator;
    public DefaultIdGeneratorTests()
    {
        _hiLoHighValueGenerator = InjectMock<IHiLoHighValueGenerator>();
    }

    //[Theory]
    //[InlineData(IdType.ChannelId, MyTelegramServerDomainConsts.ChannelInitId + 1)]
    //public async Task NextLongId_For_Init_Id_Test(IdType idType, long expectedInitId)
    //{
    //    _hiLoHighValueGenerator.Setup(c =>
    //            c.GetNewHighValueAsync(It.IsAny<IdType>(), It.IsAny<long>(), It.IsAny<CancellationToken>()))
    //        .Returns(() => Task.FromResult<long>(1));

    //    var actualInitId = await Sut.NextLongIdAsync(idType).ConfigureAwait(false);

    //    Assert.Equal(expectedInitId, actualInitId);
    //}
}
