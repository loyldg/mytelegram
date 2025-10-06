namespace MyTelegram.Messenger.Services.Interfaces;

public interface IUsernameHelper
{
    bool IsValidUsername(string username);
    IEnumerable<(int Offset, int Length, string Username)> FindMentions(string text);
}