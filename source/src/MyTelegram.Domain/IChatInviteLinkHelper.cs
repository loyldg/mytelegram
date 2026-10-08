namespace MyTelegram.Domain;

public interface IChatInviteLinkHelper
{
    string GenerateInviteLink();
    string GetHashFromLink(string link);
    string GetChatlistFullLink(string domain, string link);
    string GetFullLink(string domain, string link);
    string GetFullBusinessChatLink(string domain, string link);
}