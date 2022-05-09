using AutoFixture;

namespace MyTelegram.TestBase;

public abstract class TestsFor<TSut> : MyTelegramTestBase
{
    private readonly Lazy<TSut> _sut;
    protected TSut Sut => _sut.Value;

    protected TestsFor()
    {
        _sut = new Lazy<TSut>(CreateSut);
    }

    protected virtual TSut CreateSut() => Fixture.Create<TSut>();
}