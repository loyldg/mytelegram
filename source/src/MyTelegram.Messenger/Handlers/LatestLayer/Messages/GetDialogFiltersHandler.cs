namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Get <a href="https://corefork.telegram.org/api/folders">folders</a>
/// See <a href="https://corefork.telegram.org/method/messages.getDialogFilters" />
///</summary>
internal sealed class GetDialogFiltersHandler(
    IQueryProcessor queryProcessor,
    IAccessHashHelper2 accessHashHelper2,
    ILayeredService<IDialogFilterConverter> dialogFilterLayeredService)
    : RpcResultObjectHandler<RequestGetDialogFilters,
            IDialogFilters>
{
    protected override async Task<IDialogFilters> HandleCoreAsync(IRequestInput input,
        RequestGetDialogFilters obj)
    {
        if (input.UserId == 0)
        {
            return new TDialogFilters
            {
                Filters = [],
                TagsEnabled = false
            };
        }

        var filterReadModels = await queryProcessor.ProcessAsync(new GetDialogFiltersQuery(input.UserId), CancellationToken.None);

        var filters = new TVector<IDialogFilter>
        {
            new TDialogFilterDefault()
        };
        var converter = dialogFilterLayeredService.GetConverter(input.Layer);
        foreach (var filterReadModel in filterReadModels)
        {
            var filter = converter.ToDialogFilter(filterReadModel.Filter);
            filters.Add(filter);
            switch (filter)
            {
                case TDialogFilter dialogFilter:
                    UpdateAccessHash(input, dialogFilter.ExcludePeers);
                    UpdateAccessHash(input, dialogFilter.IncludePeers);
                    UpdateAccessHash(input, dialogFilter.PinnedPeers);
                    break;
                case TDialogFilterChatlist dialogFilterChatlist:
                    UpdateAccessHash(input, dialogFilterChatlist.PinnedPeers);
                    UpdateAccessHash(input, dialogFilterChatlist.IncludePeers);
                    break;
            }
        }

        return new TDialogFilters
        {
            Filters = filters,
            TagsEnabled = true,
        };
    }

    private void UpdateAccessHash(IRequestInput requestInput, TVector<IInputPeer> peers)
    {
        foreach (var inputPeer in peers)
        {
            switch (inputPeer)
            {
                case TInputPeerChannel inputPeerChannel:
                    inputPeerChannel.AccessHash = accessHashHelper2.GenerateAccessHash(requestInput.UserId,
                        requestInput.AccessHashKeyId, inputPeerChannel.ChannelId, AccessHashType.Channel);
                    break;
                case TInputPeerUser inputPeerUser:
                    inputPeerUser.AccessHash = accessHashHelper2.GenerateAccessHash(requestInput.UserId,
                        requestInput.AccessHashKeyId, inputPeerUser.UserId, AccessHashType.User);
                    break;
            }
        }
    }
}
