
namespace MyTelegram.Messenger.Handlers.Phone;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class SaveDefaultSendAsHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestSaveDefaultSendAs, IBool>, IObjectHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Phone.RequestSaveDefaultSendAs obj)
    {
        throw new NotImplementedException();
    }
}

