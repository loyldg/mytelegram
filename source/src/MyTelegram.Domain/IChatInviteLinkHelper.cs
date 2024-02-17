namespace MyTelegram.Domain;

public interface IChatInviteLinkHelper
{
    string GenerateInviteLink();
    string GetHashFromLink(string link);
    string GetFullLink(string domain, string link);
}