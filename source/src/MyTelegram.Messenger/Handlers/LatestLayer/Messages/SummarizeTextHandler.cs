
namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class SummarizeTextHandler(IQueryProcessor queryProcessor) : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSummarizeText, MyTelegram.Schema.ITextWithEntities>, IObjectHandler
{
    protected override async Task<MyTelegram.Schema.ITextWithEntities> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestSummarizeText obj)
    {
        var peer = obj.Peer.ToPeer();
        var messageReadModel =
            await queryProcessor.ProcessAsync(new GetMessageByPeerIdAndMessageIdQuery(peer.PeerId, obj.Id));

        return new TTextWithEntities
        {
            Text = $"Summarize text API not implemented.\n{messageReadModel?.Message}",
            Entities = []
        };
    }
}

