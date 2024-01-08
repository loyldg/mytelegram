// ReSharper disable All

namespace MyTelegram.Schema.Stats;

///<summary>
/// Contains <a href="https://corefork.telegram.org/api/stats">statistics</a> about a <a href="https://corefork.telegram.org/api/stories">story</a>.
/// See <a href="https://corefork.telegram.org/constructor/stats.StoryStats" />
///</summary>
[JsonDerivedType(typeof(TStoryStats), nameof(TStoryStats))]
public interface IStoryStats : IObject
{
    ///<summary>
    /// A graph containing the number of story views and shares
    /// See <a href="https://corefork.telegram.org/type/StatsGraph" />
    ///</summary>
    MyTelegram.Schema.IStatsGraph ViewsGraph { get; set; }

    ///<summary>
    /// A bar graph containing the number of story reactions categorized by "emotion" (i.e. Positive, Negative, Other, etc...)
    /// See <a href="https://corefork.telegram.org/type/StatsGraph" />
    ///</summary>
    MyTelegram.Schema.IStatsGraph ReactionsByEmotionGraph { get; set; }
}
