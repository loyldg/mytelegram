// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Search for messages and peers globally
/// <para>Possible errors</para>
/// Code Type Description
/// 400 FOLDER_ID_INVALID Invalid folder ID.
/// 400 SEARCH_QUERY_EMPTY The search query is empty.
/// See <a href="https://corefork.telegram.org/method/messages.searchGlobal" />
///</summary>
internal sealed class SearchGlobalHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSearchGlobal, MyTelegram.Schema.Messages.IMessages>,
    Messages.ISearchGlobalHandler
{
    private readonly IMessageAppService _messageAppService;
    //private readonly IRpcResultProcessor _rpcResultProcessor;
    private readonly ILayeredService<IRpcResultProcessor> _layeredService;

    public SearchGlobalHandler(IMessageAppService messageAppService,
        //IRpcResultProcessor rpcResultProcessor,
        ILayeredService<IRpcResultProcessor> layeredService)
    {
        _messageAppService = messageAppService;
        _layeredService = layeredService;
        //_rpcResultProcessor = rpcResultProcessor;
    }

    protected override async Task<IMessages> HandleCoreAsync(IRequestInput input,
        RequestSearchGlobal obj)
    {
        //var userId = await GetUidAsync(input);
        var userId = input.UserId;

        var r = await _messageAppService.SearchGlobalAsync(new SearchGlobalInput
        {
            OwnerPeerId = userId,
            SelfUserId = userId,
            Limit = obj.Limit,
            Q = obj.Q,
            FolderId = obj.FolderId,
            OffsetId = obj.OffsetId
        });

        //return _rpcResultProcessor.ToMessages(r, input.Layer);
        return _layeredService.GetConverter(input.Layer).ToMessages(r, input.Layer);
    }
}
