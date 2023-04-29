using AutoFixture;

namespace MyTelegram.TestBase;

public abstract class TestsFor<TSut> : MyTelegramTestBase
{
    private readonly Lazy<TSut> _sut;

    protected TestsFor()
    {
        _sut = new Lazy<TSut>(CreateSut);
    }

    protected TSut Sut => _sut.Value;

    protected virtual TSut CreateSut()
    {
        return Fixture.Create<TSut>();
    }
}
