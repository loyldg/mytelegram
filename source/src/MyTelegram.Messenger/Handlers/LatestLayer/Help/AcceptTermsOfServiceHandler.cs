namespace MyTelegram.Messenger.Handlers.LatestLayer.Help;

///<summary>
/// Accept the new terms of service
/// <para>Possible errors</para>
/// Code Type Description
/// 400 DATA_JSON_INVALID The provided JSON data is invalid.
/// See <a href="https://corefork.telegram.org/method/help.acceptTermsOfService" />
///</summary>
internal sealed class AcceptTermsOfServiceHandler : RpcResultObjectHandler<MyTelegram.Schema.Help.RequestAcceptTermsOfService, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Help.RequestAcceptTermsOfService obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}
