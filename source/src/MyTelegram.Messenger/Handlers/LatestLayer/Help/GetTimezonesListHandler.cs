namespace MyTelegram.Messenger.Handlers.LatestLayer.Help;
/// <summary>
/// Returns timezone information that may be used elsewhere in the API, such as to set <a href="https://corefork.telegram.org/api/business#opening-hours">Telegram Business opening hours »</a>.
/// <para><c>See <a href="https://corefork.telegram.org/method/help.getTimezonesList"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetTimezonesListHandler(ITimezoneHelper timezoneHelper) : RpcResultObjectHandler<MyTelegram.Schema.Help.RequestGetTimezonesList, MyTelegram.Schema.Help.ITimezonesList>
{
    private static TVector<MyTelegram.Schema.ITimezone> _timezones = [];
    private static int _hash = 1393580518;
    protected override Task<MyTelegram.Schema.Help.ITimezonesList> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Help.RequestGetTimezonesList obj)
    {
        if (obj.Hash == _hash)
        {
            return Task.FromResult<ITimezonesList>(new TTimezonesListNotModified());
        }

        if (_timezones.Count == 0)
        {
            var items = timezoneHelper.GetTimeZoneList();
            _timezones = [..items!.Select(p => new TTimezone { Id = p.Id, Name = p.Name, UtcOffset = p.UtcOffset })];
        }

        return Task.FromResult<MyTelegram.Schema.Help.ITimezonesList>(new TTimezonesList { Timezones = _timezones, Hash = _hash });
    }
}