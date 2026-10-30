
namespace MyTelegram.Messenger.Handlers.LatestLayer.Toncenter;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class GetStreamingUrlHandler : RpcResultObjectHandler<MyTelegram.Schema.Toncenter.RequestGetStreamingUrl, MyTelegram.Schema.Toncenter.IStreamingUrl>
{
    protected override Task<MyTelegram.Schema.Toncenter.IStreamingUrl> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Toncenter.RequestGetStreamingUrl obj)
    {
        throw new NotImplementedException();
    }
}

