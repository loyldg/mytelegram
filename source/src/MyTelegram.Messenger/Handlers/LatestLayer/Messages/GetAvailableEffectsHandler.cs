namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Fetch the full list of usable <a href="https://corefork.telegram.org/api/effects">animated message effects »</a>.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.getAvailableEffects"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetAvailableEffectsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetAvailableEffects, MyTelegram.Schema.Messages.IAvailableEffects>
{
    protected override Task<MyTelegram.Schema.Messages.IAvailableEffects> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestGetAvailableEffects obj)
    {
        return Task.FromResult<MyTelegram.Schema.Messages.IAvailableEffects>(new TAvailableEffects { Documents = [], Effects = [], });
    }
}