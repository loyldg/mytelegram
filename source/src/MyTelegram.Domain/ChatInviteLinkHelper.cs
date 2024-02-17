namespace MyTelegram.Domain;

public class ChatInviteLinkHelper : IChatInviteLinkHelper
{
    public string GenerateInviteLink()
    {
        var bytes = new byte[12];
        Random.Shared.NextBytes(bytes);
        var inviteHash = $"{Convert.ToBase64String(bytes)
            .Replace("+", "-")
            .Replace("/", ".")
            .Replace("=", string.Empty)}";

        return inviteHash;
    }

    public string GetHashFromLink(string link)
    {
        var index = link.LastIndexOf("/", StringComparison.OrdinalIgnoreCase);
        return link[(index + 2)..];
    }

    public string GetFullLink(string domain, string link)
    {
        var newDomain = domain;
        if (!newDomain.EndsWith("/"))
        {
            newDomain = $"{domain}/";
        }

        return $"{newDomain}+{link}";
    }
}