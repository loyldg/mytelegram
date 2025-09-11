namespace MyTelegram.Messenger.Handlers.LatestLayer.Auth;

///<summary>
/// Request an SMS code via Firebase.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 PHONE_CODE_EMPTY phone_code is missing.
/// 400 PHONE_NUMBER_INVALID The phone number is invalid.
/// See <a href="https://corefork.telegram.org/method/auth.requestFirebaseSms" />
///</summary>
internal sealed class RequestFirebaseSmsHandler : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestRequestFirebaseSms, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Auth.RequestRequestFirebaseSms obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}
