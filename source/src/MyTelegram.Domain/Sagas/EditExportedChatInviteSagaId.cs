using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTelegram.Domain.Sagas;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<EditExportedChatInviteSagaId>))]

public class EditExportedChatInviteSagaId : SingleValueObject<string>, ISagaId
{
    public EditExportedChatInviteSagaId(string value) : base(value)
    {
    }
}