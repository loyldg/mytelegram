
namespace MyTelegram.Messenger.Handlers.Payments;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class GetStarGiftUpgradeAttributesHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestGetStarGiftUpgradeAttributes, MyTelegram.Schema.Payments.IStarGiftUpgradeAttributes>, IObjectHandler
{
    protected override Task<MyTelegram.Schema.Payments.IStarGiftUpgradeAttributes> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestGetStarGiftUpgradeAttributes obj)
    {
        throw new NotImplementedException();
    }
}

