// ReSharper disable All

namespace MyTelegram.Schema.Messages;

public interface ICheckedHistoryImportPeer : IObject
{
    string ConfirmText { get; set; }
}
