using System.Linq.Expressions;
using MyTelegram.Messenger.Services.Impl;
using Shouldly;

namespace MyTelegram.Messenger.Tests.Services.Impl;

/// <summary>
/// Executes the pagination requests sent by tdesktop, Android, iOS and TDLib against an in-memory message list
/// and verifies the returned message ids.
/// </summary>
public class OffsetPaginationTests
{
    public record TestMessage(int MessageId, int Date);

    // Message ids are not continuous because of deleted messages.
    private static readonly int[] DeletedIds = [5, 13, 21, 22, 30, 45];

    private static readonly List<TestMessage> Messages = Enumerable.Range(1, 60)
        .Where(id => !DeletedIds.Contains(id))
        .Select(id => new TestMessage(id, GetDate(id)))
        .ToList();

    private readonly OffsetHelper _offsetHelper = new();

    private static int GetDate(int messageId) => 1000 + messageId * 10;

    private static Task<IReadOnlyCollection<TestMessage>> FindSliceAsync(Expression<Func<TestMessage, bool>> filter,
        int skip,
        int limit,
        bool ascending)
    {
        var query = Messages.AsQueryable().Where(filter);
        query = ascending ? query.OrderBy(p => p.MessageId) : query.OrderByDescending(p => p.MessageId);

        return Task.FromResult<IReadOnlyCollection<TestMessage>>(query.Skip(skip).Take(limit).ToList());
    }

    private async Task<int[]> GetPageAsync(int offsetId = 0,
        int addOffset = 0,
        int limit = 20,
        int offsetDate = 0,
        int minId = 0,
        int maxId = 0,
        int minDate = 0,
        int maxDate = 0)
    {
        var offset = _offsetHelper.GetOffsetInfo(addOffset, offsetId, limit, minId, maxId, minDate, maxDate, offsetDate);
        var result = await OffsetPagination.FindAsync<TestMessage, TestMessage>(offset,
            _ => true,
            p => p.MessageId,
            p => p.Date,
            FindSliceAsync);

        return result.Select(p => p.MessageId).ToArray();
    }

    /// <summary>
    /// Reference implementation of https://core.telegram.org/api/offsets:
    /// the window [offsetFromID(offset_id) + add_offset, +limit) of the list sorted by descending id.
    /// </summary>
    private static int[] GetExpectedPage(int offsetId, int addOffset, int limit, int offsetDate, int minId, int maxId)
    {
        limit = limit <= 0 ? OffsetHelper.DefaultLimit : Math.Min(limit, OffsetHelper.MaxLimit);
        var list = Messages
            .Where(p => (minId == 0 || p.MessageId > minId) && (maxId == 0 || p.MessageId < maxId))
            .OrderByDescending(p => p.MessageId)
            .ToList();
        var offsetFromId = offsetId > 0
            ? list.Count(p => p.MessageId >= offsetId)
            : offsetDate > 0
                ? list.Count(p => p.Date > offsetDate)
                : 0;
        var start = offsetFromId + addOffset;

        return Enumerable.Range(start, limit)
            .Where(position => position >= 0 && position < list.Count)
            .Select(position => list[position].MessageId)
            .ToArray();
    }

    #region tdesktop

    [Fact]
    public async Task TDesktop_LoadMessagesUp()
    {
        // history_widget.cpp loadMessagesUp: offset_id = minMsgId, add_offset = 0
        (await GetPageAsync(offsetId: 31, addOffset: 0, limit: 5)).ShouldBe([29, 28, 27, 26, 25]);
    }

    [Fact]
    public async Task TDesktop_LoadMessagesDown_ReturnsMessageRightAfterLoadedMax()
    {
        // history_widget.cpp loadMessagesDown: offset_id = maxMsgId + 1, add_offset = -limit
        // Loaded max is 10, message 11 must not be skipped.
        (await GetPageAsync(offsetId: 11, addOffset: -5, limit: 5)).ShouldBe([16, 15, 14, 12, 11]);
    }

    [Fact]
    public async Task TDesktop_LoadMessagesDown_WhenOffsetIdIsDeleted()
    {
        // Loaded max is 20, messages 21 and 22 are deleted.
        (await GetPageAsync(offsetId: 21, addOffset: -5, limit: 5)).ShouldBe([27, 26, 25, 24, 23]);
    }

    [Fact]
    public async Task TDesktop_LoadAround_SearchRepliesSavedSublist()
    {
        // data_search_controller.cpp / data_replies_list.cpp: add_offset = -limit / 2
        (await GetPageAsync(offsetId: 25, addOffset: -5, limit: 10))
            .ShouldBe([29, 28, 27, 26, 25, 24, 23, 20, 19, 18]);
    }

    [Fact]
    public async Task TDesktop_RequestMessageAfterTime_ReturnsFirstMessageAtOrAfterDate()
    {
        // apiwrap.cpp: offset_id = 0, offset_date = when - 1, add_offset = -1, limit = 1
        const int when = 1205;
        (await GetPageAsync(offsetId: 0, addOffset: -1, limit: 1, offsetDate: when - 1)).ShouldBe([23]);
    }

    [Fact]
    public async Task TDesktop_UnreadMentions_FirstPage_ReturnsOldest()
    {
        // api_unread_things.cpp: offset_id = max(maxLoaded, 1), add_offset = -limit
        (await GetPageAsync(offsetId: 1, addOffset: -3, limit: 3)).ShouldBe([3, 2, 1]);
    }

    [Fact]
    public async Task TDesktop_UnreadMentions_NextPage_ExcludesLoadedMax()
    {
        // api_unread_things.cpp: offset_id = maxLoaded, add_offset = -(limit + 1)
        (await GetPageAsync(offsetId: 3, addOffset: -4, limit: 3)).ShouldBe([7, 6, 4]);
    }

    #endregion

    #region Android

    [Fact]
    public async Task Android_LoadBackwardInChat_IncludesMaxId()
    {
        // MessagesController.loadMessagesInternal load_type 0 (chat): add_offset = -1, limit = count + 1
        (await GetPageAsync(offsetId: 31, addOffset: -1, limit: 6)).ShouldBe([31, 29, 28, 27, 26, 25]);
    }

    [Fact]
    public async Task Android_LoadForward()
    {
        // load_type 1: add_offset = -count - 1
        (await GetPageAsync(offsetId: 12, addOffset: -6, limit: 5)).ShouldBe([18, 17, 16, 15, 14]);
    }

    [Fact]
    public async Task Android_LoadFromUnread_History()
    {
        // load_type 2: add_offset = -count + 6
        (await GetPageAsync(offsetId: 25, addOffset: -4, limit: 10))
            .ShouldBe([28, 27, 26, 25, 24, 23, 20, 19, 18, 17]);
    }

    [Fact]
    public async Task Android_LoadFromUnread_Replies()
    {
        // load_type 2: add_offset = -count + 10
        (await GetPageAsync(offsetId: 25, addOffset: -5, limit: 15))
            .ShouldBe([29, 28, 27, 26, 25, 24, 23, 20, 19, 18, 17, 16, 15, 14, 12]);
    }

    [Fact]
    public async Task Android_LoadAroundMessage_OddCount()
    {
        // load_type 3: add_offset = -count / 2
        (await GetPageAsync(offsetId: 25, addOffset: -9 / 2, limit: 9))
            .ShouldBe([28, 27, 26, 25, 24, 23, 20, 19, 18]);
    }

    [Fact]
    public async Task Android_LoadAroundDate()
    {
        // load_type 4: offset_id = 0, offset_date, add_offset = -count + 5
        (await GetPageAsync(offsetId: 0, addOffset: -5, limit: 10, offsetDate: GetDate(25)))
            .ShouldBe([31, 29, 28, 27, 26, 25, 24, 23, 20, 19]);
    }

    #endregion

    #region iOS

    [Fact]
    public async Task IOS_FillHoleUpward_StaysInsideTheHole()
    {
        // Holes.swift .range(start 14 ... end 20): offset_id = start - 1, add_offset = -limit, max_id = end + 1, min_id = start - 1
        (await GetPageAsync(offsetId: 13, addOffset: -10, limit: 10, minId: 13, maxId: 21))
            .ShouldBe([20, 19, 18, 17, 16, 15, 14]);
    }

    [Fact]
    public async Task IOS_FillHoleDownward()
    {
        // Holes.swift .range(start 40 ... end 30): offset_id = start + 1, add_offset = 0, max_id = start + 1, min_id = end
        (await GetPageAsync(offsetId: 41, addOffset: 0, limit: 5, minId: 30, maxId: 41))
            .ShouldBe([40, 39, 38, 37, 36]);
    }

    [Fact]
    public async Task IOS_FillHoleDownward_StopsAtMinId()
    {
        (await GetPageAsync(offsetId: 41, addOffset: 0, limit: 20, minId: 30, maxId: 41))
            .ShouldBe([40, 39, 38, 37, 36, 35, 34, 33, 32, 31]);
    }

    [Fact]
    public async Task IOS_AroundId()
    {
        // Holes.swift .aroundId: add_offset = -limit / 2, max_id = Int32.max, min_id = 0
        (await GetPageAsync(offsetId: 25, addOffset: -5, limit: 10, maxId: int.MaxValue))
            .ShouldBe([29, 28, 27, 26, 25, 24, 23, 20, 19, 18]);
    }

    [Fact]
    public async Task IOS_PagesWithMaxIdAndZeroOffsetId()
    {
        // Holes.swift phone calls search: offset_id = 0, add_offset = 0, max_id = hole id
        (await GetPageAsync(offsetId: 0, addOffset: 0, limit: 5, maxId: 20)).ShouldBe([19, 18, 17, 16, 15]);
    }

    #endregion

    #region TDLib

    [Fact]
    public async Task TDLib_GetHistory_IncludesFromMessageId()
    {
        // MessagesManager::get_history_impl: offset = -1
        (await GetPageAsync(offsetId: 31, addOffset: -1, limit: 6)).ShouldBe([31, 29, 28, 27, 26, 25]);
    }

    [Fact]
    public async Task TDLib_GetHistory_LargeNegativeOffset()
    {
        // offset -10, limit 20 => add_offset -88, limit 100
        var result = await GetPageAsync(offsetId: 25, addOffset: -88, limit: 100);

        // All 34 messages >= 25 and the 12 newest messages < 25
        result.Length.ShouldBe(46);
        result[0].ShouldBe(60);
        result[^13..].ShouldBe([25, 24, 23, 20, 19, 18, 17, 16, 15, 14, 12, 11, 10]);
    }

    [Fact]
    public async Task TDLib_GetHistoryFromTheEnd()
    {
        (await GetPageAsync(offsetId: 0, addOffset: 0, limit: 5)).ShouldBe([60, 59, 58, 57, 56]);
    }

    [Fact]
    public async Task TDLib_GetDialogMessageByDate()
    {
        // GetDialogMessageByDateQuery: offset_id = 0, offset_date = date, add_offset = -3, limit = 5
        (await GetPageAsync(offsetId: 0, addOffset: -3, limit: 5, offsetDate: GetDate(25)))
            .ShouldBe([28, 27, 26, 25, 24]);
    }

    #endregion

    #region Edge cases

    [Fact]
    public async Task PositiveAddOffset_SkipsOlderMessages()
    {
        (await GetPageAsync(offsetId: 25, addOffset: 2, limit: 3)).ShouldBe([20, 19, 18]);
    }

    [Fact]
    public async Task WindowAboveTheTop_IsEmpty()
    {
        (await GetPageAsync(offsetId: 55, addOffset: -20, limit: 3)).ShouldBeEmpty();
    }

    [Fact]
    public async Task NoAnchor_NegativeAddOffset_ReturnsTopOfTheList()
    {
        (await GetPageAsync(offsetId: 0, addOffset: -5, limit: 10)).ShouldBe([60, 59, 58, 57, 56]);
    }

    [Fact]
    public async Task ZeroLimit_UsesDefaultLimit()
    {
        (await GetPageAsync(limit: 0)).Length.ShouldBe(OffsetHelper.DefaultLimit);
    }

    [Fact]
    public async Task MinDateAndMaxDate_AreApplied()
    {
        (await GetPageAsync(limit: 100, minDate: GetDate(40), maxDate: GetDate(44)))
            .ShouldBe([43, 42, 41]);
    }

    public static TheoryData<int, int, int, int, int, int> OfficialFormulaCases()
    {
        var data = new TheoryData<int, int, int, int, int, int>();
        int[] offsetIds = [0, 1, 5, 25, 59, 60, 61, 1000];
        int[] addOffsets = [-150, -101, -100, -21, -20, -11, -10, -5, -1, 0, 1, 7, 100];
        int[] limits = [1, 2, 5, 10, 20, 100];
        foreach (var offsetId in offsetIds)
        {
            foreach (var addOffset in addOffsets)
            {
                foreach (var limit in limits)
                {
                    data.Add(offsetId, addOffset, limit, 0, 0, 0);
                }
            }
        }

        // offset_date anchor
        foreach (var addOffset in addOffsets)
        {
            data.Add(0, addOffset, 10, GetDate(25), 0, 0);
            data.Add(0, addOffset, 10, GetDate(25) + 5, 0, 0);
        }

        // min_id/max_id bounds
        data.Add(25, -5, 10, 0, 18, 0);
        data.Add(25, -5, 10, 0, 0, 28);
        data.Add(25, 0, 10, 0, 20, 40);
        data.Add(25, -10, 10, 0, 20, 40);

        return data;
    }

    [Theory]
    [MemberData(nameof(OfficialFormulaCases))]
    public async Task MatchesOfficialFormula(int offsetId, int addOffset, int limit, int offsetDate, int minId, int maxId)
    {
        var result = await GetPageAsync(offsetId, addOffset, limit, offsetDate, minId, maxId);

        result.ShouldBe(GetExpectedPage(offsetId, addOffset, limit, offsetDate, minId, maxId));
    }

    #endregion
}
