
namespace MyTelegram.Messenger.Handlers.LatestLayer.Toncenter;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class PerformApiRequestHandler : RpcResultObjectHandler<MyTelegram.Schema.Toncenter.RequestPerformApiRequest, MyTelegram.Schema.Toncenter.IApiResponse>
{
    protected override Task<MyTelegram.Schema.Toncenter.IApiResponse> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Toncenter.RequestPerformApiRequest obj)
    {
        throw new NotImplementedException();
    }
}

