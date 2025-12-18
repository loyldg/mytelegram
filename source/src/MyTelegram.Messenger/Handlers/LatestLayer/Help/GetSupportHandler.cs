namespace MyTelegram.Messenger.Handlers.LatestLayer.Help;
/// <summary>
/// Returns the support user for the "ask a question" feature.
/// <para><c>See <a href="https://corefork.telegram.org/method/help.getSupport"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetSupportHandler(IUserAppService userAppService, IUserConverterService userConverterService, IOptionsMonitor<MyTelegramMessengerServerOptions> options) : RpcResultObjectHandler<MyTelegram.Schema.Help.RequestGetSupport, MyTelegram.Schema.Help.ISupport>
{
    protected override async Task<MyTelegram.Schema.Help.ISupport> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Help.RequestGetSupport obj)
    {
        var supportUserId = MyTelegramConsts.DefaultSupportUserId;
        if (!string.IsNullOrEmpty(options.CurrentValue.SupportUserId))
        {
            if (!long.TryParse(options.CurrentValue.SupportUserId, out supportUserId))
            {
                supportUserId = MyTelegramConsts.DefaultSupportUserId;
            }
        }

        var userReadModel = await userAppService.GetAsync((long? )supportUserId);
        if (userReadModel == null)
        {
            supportUserId = MyTelegramConsts.DefaultSupportUserId;
        }

        var user = await userConverterService.GetUserAsync(input, supportUserId, layer: input.Layer);
        return new TSupport
        {
            PhoneNumber = user.Phone ?? string.Empty,
            User = user
        };
    }
}