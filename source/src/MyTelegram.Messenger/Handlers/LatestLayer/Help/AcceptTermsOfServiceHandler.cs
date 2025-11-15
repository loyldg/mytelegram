namespace MyTelegram.Messenger.Handlers.LatestLayer.Help;
/// <summary>
/// Accept the new terms of service
/// Possible errors
/// Code Type Description
/// 400 DATA_JSON_INVALID The provided JSON data is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/help.acceptTermsOfService"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class AcceptTermsOfServiceHandler : RpcResultObjectHandler<MyTelegram.Schema.Help.RequestAcceptTermsOfService, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Help.RequestAcceptTermsOfService obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}