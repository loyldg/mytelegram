namespace MyTelegram.Messenger.Handlers.LatestLayer.Help;
/// <summary>
/// Dismiss a <a href="https://corefork.telegram.org/api/config#suggestions">suggestion, see here for more info »</a>.
/// <para><c>See <a href="https://corefork.telegram.org/method/help.dismissSuggestion"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class DismissSuggestionHandler : RpcResultObjectHandler<RequestDismissSuggestion, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, RequestDismissSuggestion obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}