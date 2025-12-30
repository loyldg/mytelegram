
namespace MyTelegram.Messenger.Handlers.Phone;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class GetGroupCallStarsHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestGetGroupCallStars, MyTelegram.Schema.Phone.IGroupCallStars>, IObjectHandler
{
    protected override Task<MyTelegram.Schema.Phone.IGroupCallStars> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Phone.RequestGetGroupCallStars obj)
    {
        throw new NotImplementedException();
    }
}

