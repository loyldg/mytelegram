namespace MyTelegram.Messenger.Converters.ConverterServices.Contacts;

internal sealed class SearchConverterService(IUserConverterService userConverterService,
    IChatConverterService chatConverterService
    ) : ISearchConverterService, ITransientDependency
{
    public async Task<IFound> ToFoundAsync(IRequestWithAccessHashKeyId request, SearchContactOutput output, int layer)
    {
        var users = await userConverterService.ToUserListAsync(request,
            output.UserList,
            output.PhotoList,
            output.ContactList,
            output.PrivacyList,
            layer);

        var myChannels = chatConverterService.ToChannelList(request,
            output.MyChannelList,
            output.PhotoList,
            output.ChannelMemberList,
            layer: layer);

        var otherChannels = chatConverterService.ToChannelList(request,
            output.ChannelList,
            output.PhotoList,
            [],
            null,
            layer);

        otherChannels.AddRange(myChannels);

        var peers = output.UserList.Select(p => (IPeer)new TPeerUser { UserId = p.UserId }).ToList();
        peers.AddRange(output.MyChannelList.Select(p => (IPeer)new TPeerChannel { ChannelId = p.ChannelId }));

        var otherPeers = output.ChannelList.Select(p => (IPeer)new TPeerChannel { ChannelId = p.ChannelId });

        return new TFound
        {
            Chats = [.. otherChannels],
            MyResults = [.. peers],
            Results = [.. otherPeers],
            Users = [.. users]
        };
    }
}
