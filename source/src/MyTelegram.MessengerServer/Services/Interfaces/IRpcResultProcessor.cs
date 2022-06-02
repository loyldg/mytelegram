using MyTelegram.Schema.Contacts;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Services.Interfaces;

public interface IRpcResultProcessor
{
    IFound ToFound(SearchContactOutput output);
    IMessages ToMessages(GetMessageOutput output);
}