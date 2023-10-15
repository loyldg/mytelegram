using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTelegram.QueryHandlers.MongoDB.ChatAdmin;

public class GetChatAdminListByChannelIdQueryHandler : IQueryHandler<GetChatAdminListByChannelIdQuery,
    IReadOnlyCollection<IChatAdminReadModel>>
{
    private readonly IMyMongoDbReadModelStore<ChatAdminReadModel> _store;

    public GetChatAdminListByChannelIdQueryHandler(IMyMongoDbReadModelStore<ChatAdminReadModel> store)
    {
        _store = store;
    }

    public async Task<IReadOnlyCollection<IChatAdminReadModel>> ExecuteQueryAsync(GetChatAdminListByChannelIdQuery query, CancellationToken cancellationToken)
    {
        var options = new FindOptions<ChatAdminReadModel, ChatAdminReadModel>
        {
            Limit = query.Limit,
            Skip = query.Skip,
        };
        var cursor = await _store.FindAsync(p => p.PeerId == query.PeerId, options,
            cancellationToken: cancellationToken);

        return await cursor.ToListAsync(cancellationToken: cancellationToken);
    }
}
