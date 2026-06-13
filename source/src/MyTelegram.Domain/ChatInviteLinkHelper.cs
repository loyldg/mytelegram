using System.Buffers.Text;

namespace MyTelegram.Domain;

public class ChatInviteLinkHelper : IChatInviteLinkHelper
{
    public string GenerateInviteLink()
    {
        var bytes = new byte[12];
        Random.Shared.NextBytes(bytes);

        return Base64Url.EncodeToString(bytes);
    }

    public string GetHashFromLink(string link)
    {
        var index = link.LastIndexOf("/", StringComparison.OrdinalIgnoreCase);

        var newLink = link[(index + 1)..];
        if (newLink.StartsWith("+"))
        {
            return newLink[1..];
        }

        return newLink;
    }

    public string GetChatlistFullLink(string domain, string link)
    {
        return GetFullLinkCore(domain, "addlist/", link);
    }

    public string GetFullLink(string domain, string link)
    {
        return GetFullLinkCore(domain, "+", link);
    }

    private string GetFullLinkCore(string domain, string type, string link)
    {
        var newDomain = domain;
        if (!newDomain.EndsWith("/"))
        {
            newDomain = $"{domain}/";
        }

        return $"{newDomain}{type}{link}";
    }
}