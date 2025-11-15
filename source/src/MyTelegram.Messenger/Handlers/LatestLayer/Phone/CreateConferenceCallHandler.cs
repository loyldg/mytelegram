namespace MyTelegram.Messenger.Handlers.LatestLayer.Phone;
/// <summary>
/// Create and optionally join a new conference call.
/// <para><c>See <a href="https://corefork.telegram.org/method/phone.createConferenceCall"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class CreateConferenceCallHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestCreateConferenceCall, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Phone.RequestCreateConferenceCall obj)
    {
        throw new NotImplementedException();
    }
}