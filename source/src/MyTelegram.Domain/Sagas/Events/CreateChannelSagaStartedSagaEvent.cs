namespace MyTelegram.Domain.Sagas.Events;

public class CreateChannelSagaStartedSagaEvent(
    RequestInfo requestInfo,
    long channelId,
    string title,
    bool broadcast,
    long randomId,
    bool migratedFromChat,
    bool autoCreateFromChat,
    int? ttlPeriod,
    bool isTtlFromDefaultSetting,
    List<long> memberUserIds,
    List<long> botUserIds
    )
    : AggregateEvent<CreateChannelSaga, CreateChannelSagaId>
{
    public RequestInfo RequestInfo { get; } = requestInfo;
    public long ChannelId { get; } = channelId;
    public string Title { get; } = title;
    public bool Broadcast { get; } = broadcast;
    public long RandomId { get; } = randomId;
    public bool MigratedFromChat { get; } = migratedFromChat;
    public bool AutoCreateFromChat { get; } = autoCreateFromChat;
    public int? TtlPeriod { get; } = ttlPeriod;
    public bool IsTtlFromDefaultSetting { get; } = isTtlFromDefaultSetting;
    public List<long> MemberUserIds { get; } = memberUserIds;
    public List<long> BotUserIds { get; } = botUserIds;
}