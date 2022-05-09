namespace MyTelegram.Domain.Tests.UnitTests;

public class SimpleInMemoryIdGeneratorTests : TestsFor<SimpleInMemoryIdGenerator>
{
    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(100)]
    public async Task SequenceId_Should_Increment_By_Step(int step)
    {
        var id1 = await Sut.NextLongIdAsync(IdType.MessageId, 1, step).ConfigureAwait(false);
        var id2 = await Sut.NextLongIdAsync(IdType.MessageId, 1, step).ConfigureAwait(false);
        var id3 = await Sut.NextLongIdAsync(IdType.MessageId, 1, step).ConfigureAwait(false);

        id1.ShouldBe(step);
        id2.ShouldBe(id1 + step);
        id3.ShouldBe(id2 + step);
    }
}