// ReSharper disable All

namespace MyTelegram.Handlers.Phone;

///<summary>
/// Subscribe or unsubscribe to a scheduled group call
/// <para>Possible errors</para>
/// Code Type Description
/// 403 GROUPCALL_ALREADY_STARTED The groupcall has already started, you can join directly using <a href="https://corefork.telegram.org/method/phone.joinGroupCall">phone.joinGroupCall</a>.
/// See <a href="https://corefork.telegram.org/method/phone.toggleGroupCallStartSubscription" />
///</summary>
internal sealed class ToggleGroupCallStartSubscriptionHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestToggleGroupCallStartSubscription, MyTelegram.Schema.IUpdates>,
    Phone.IToggleGroupCallStartSubscriptionHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Phone.RequestToggleGroupCallStartSubscription obj)
    {
        throw new NotImplementedException();
    }
}
