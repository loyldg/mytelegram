using MyTelegram.Messenger.Services.Impl;
using Shouldly;

namespace MyTelegram.Messenger.Tests.Services.Impl;

/// <summary>
/// Verifies how offset_id/add_offset/limit are converted to the newer (id &gt;= offset_id) and older (id &lt; offset_id) slices.
/// See https://core.telegram.org/api/offsets
/// </summary>
public class OffsetHelperTests
{
    private readonly OffsetHelper _sut = new();

    private OffsetInfo Get(int offsetId, int addOffset, int limit, int offsetDate = 0, int minId = 0, int maxId = 0)
    {
        return _sut.GetOffsetInfo(addOffset, offsetId, limit, minId, maxId, 0, 0, offsetDate);
    }

    private static void ShouldBe(OffsetInfo offset, int newerSkip, int newerTake, int olderSkip, int olderTake,
        LoadType loadType)
    {
        offset.NewerSkip.ShouldBe(newerSkip);
        offset.NewerTake.ShouldBe(newerTake);
        offset.OlderSkip.ShouldBe(olderSkip);
        offset.OlderTake.ShouldBe(olderTake);
        offset.LoadType.ShouldBe(loadType);
    }

    [Fact]
    public void Doc_LoadOlderThanMessage()
    {
        // messages.getHistory({offset_id: MSGID, add_offset: 0, limit: 20})
        ShouldBe(Get(100, 0, 20), 0, 0, 0, 20, LoadType.Backward);
    }

    [Fact]
    public void Doc_LoadNewerThanMessage()
    {
        // messages.getHistory({offset_id: MSGID, add_offset: -20, limit: 20})
        ShouldBe(Get(100, -20, 20), 0, 20, 0, 0, LoadType.Forward);
    }

    [Fact]
    public void Doc_LoadAroundMessage()
    {
        // messages.getHistory({offset_id: MSGID, add_offset: -10, limit: 20})
        ShouldBe(Get(100, -10, 20), 0, 10, 0, 10, LoadType.AroundMessage);
    }

    [Fact]
    public void TDesktop_LoadMessagesDown_IncludesOffsetId()
    {
        // history_widget.cpp loadMessagesDown: offset_id = maxMsgId + 1, add_offset = -limit
        ShouldBe(Get(51, -50, 50), 0, 50, 0, 0, LoadType.Forward);
    }

    [Fact]
    public void TDesktop_UnreadMentions_NextPage_SkipsOffsetId()
    {
        // api_unread_things.cpp: add_offset = -(limit + 1)
        ShouldBe(Get(30, -101, 100), 1, 100, 0, 0, LoadType.Forward);
    }

    [Fact]
    public void TDesktop_RequestMessageAfterTime_UsesDateAnchor()
    {
        // apiwrap.cpp requestMessageAfterTime: offset_id = 0, offset_date = when - 1, add_offset = -1, limit = 1
        var offset = Get(0, -1, 1, offsetDate: 1_700_000_000);
        ShouldBe(offset, 0, 1, 0, 0, LoadType.Forward);
        offset.HasDateAnchor.ShouldBeTrue();
    }

    [Fact]
    public void Android_LoadBackwardInChat_IncludesOffsetIdItself()
    {
        // MessagesController.loadMessagesInternal: add_offset = -1, limit = count + 1
        ShouldBe(Get(100, -1, 31), 0, 1, 0, 30, LoadType.AroundMessage);
    }

    [Fact]
    public void Android_LoadForward()
    {
        // load_type 1: add_offset = -count - 1
        ShouldBe(Get(100, -31, 30), 1, 30, 0, 0, LoadType.Forward);
    }

    [Theory]
    [InlineData(30, -24, 24, 6)] // load_type 2 (history): add_offset = -count + 6
    [InlineData(30, -20, 20, 10)] // load_type 2 (replies): add_offset = -count + 10
    [InlineData(12, -6, 6, 6)] // collides with -limit / 2 in the old implementation
    public void Android_LoadFromUnread(int count, int addOffset, int expectedNewer, int expectedOlder)
    {
        ShouldBe(Get(100, addOffset, count), 0, expectedNewer, 0, expectedOlder, LoadType.AroundMessage);
    }

    [Theory]
    [InlineData(30, -15, 15, 15)]
    [InlineData(31, -15, 15, 16)] // odd limit, add_offset = -count / 2
    [InlineData(2, -1, 1, 1)]
    public void Android_LoadAroundMessage(int count, int addOffset, int expectedNewer, int expectedOlder)
    {
        ShouldBe(Get(100, addOffset, count), 0, expectedNewer, 0, expectedOlder, LoadType.AroundMessage);
    }

    [Fact]
    public void Android_LoadAroundDate()
    {
        // load_type 4: offset_id = 0, offset_date, add_offset = -count + 5
        ShouldBe(Get(0, -25, 30, offsetDate: 1_700_000_000), 0, 25, 0, 5, LoadType.AroundDate);
    }

    [Fact]
    public void IOS_HoleUpward()
    {
        // Holes.swift .range(start <= end): offset_id = start - 1, add_offset = -limit, max_id = end + 1, min_id = start - 1
        var offset = Get(9, -100, 100, minId: 9, maxId: 21);
        ShouldBe(offset, 0, 100, 0, 0, LoadType.Forward);
        offset.MinId.ShouldBe(9);
        offset.MaxId.ShouldBe(21);
    }

    [Fact]
    public void IOS_HoleDownward()
    {
        // Holes.swift .range(start > end): offset_id = start + 1, add_offset = 0, max_id = start + 1, min_id = end
        ShouldBe(Get(41, 0, 100, minId: 30, maxId: 41), 0, 0, 0, 100, LoadType.Backward);
    }

    [Fact]
    public void TDLib_GetHistory_OffsetMinus1()
    {
        // MessagesManager::get_history_impl: offset = -1, limit = 100
        ShouldBe(Get(100, -1, 100), 0, 1, 0, 99, LoadType.AroundMessage);
    }

    [Fact]
    public void TDLib_GetHistory_LargeNegativeOffset()
    {
        // offset -10, limit 20 => offset -= 100 - 20 - 2 => -88, limit 100
        ShouldBe(Get(100, -88, 100), 0, 88, 0, 12, LoadType.AroundMessage);
    }

    [Fact]
    public void TDLib_GetDialogMessageByDate()
    {
        // GetDialogMessageByDateQuery: offset_id = 0, offset_date = date, add_offset = -3, limit = 5
        ShouldBe(Get(0, -3, 5, offsetDate: 1_700_000_000), 0, 3, 0, 2, LoadType.AroundDate);
    }

    [Fact]
    public void PositiveAddOffset_SkipsOlderResults()
    {
        ShouldBe(Get(100, 10, 20), 0, 0, 10, 20, LoadType.Backward);
    }

    [Fact]
    public void AddOffsetFarBelowLimit_SkipsNewerResults()
    {
        ShouldBe(Get(100, -50, 20), 30, 20, 0, 0, LoadType.Forward);
    }

    [Fact]
    public void NoAnchor_NegativeAddOffset_OnlyReturnsTheTopOfTheList()
    {
        ShouldBe(Get(0, -5, 20), 0, 0, 0, 15, LoadType.Backward);
    }

    [Theory]
    [InlineData(0, OffsetHelper.DefaultLimit)]
    [InlineData(-5, OffsetHelper.DefaultLimit)]
    [InlineData(1000, OffsetHelper.MaxLimit)]
    [InlineData(50, 50)]
    public void Limit_IsClamped(int limit, int expected)
    {
        Get(0, 0, limit).Limit.ShouldBe(expected);
    }
}
