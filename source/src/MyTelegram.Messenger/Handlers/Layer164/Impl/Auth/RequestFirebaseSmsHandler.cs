// ReSharper disable All

namespace MyTelegram.Handlers.Auth;

///<summary>
/// Request an SMS code via Firebase.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 PHONE_NUMBER_INVALID The phone number is invalid.
/// See <a href="https://corefork.telegram.org/method/auth.requestFirebaseSms" />
///</summary>
internal sealed class RequestFirebaseSmsHandler : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestRequestFirebaseSms, IBool>,
    Auth.IRequestFirebaseSmsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Auth.RequestRequestFirebaseSms obj)
    {
        throw new NotImplementedException();
    }
}
