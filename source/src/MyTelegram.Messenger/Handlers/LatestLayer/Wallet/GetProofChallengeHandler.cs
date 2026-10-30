
namespace MyTelegram.Messenger.Handlers.LatestLayer.Wallet;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class GetProofChallengeHandler : RpcResultObjectHandler<MyTelegram.Schema.Wallet.RequestGetProofChallenge, MyTelegram.Schema.Wallet.IProofChallenge>
{
    protected override Task<MyTelegram.Schema.Wallet.IProofChallenge> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Wallet.RequestGetProofChallenge obj)
    {
        throw new NotImplementedException();
    }
}

