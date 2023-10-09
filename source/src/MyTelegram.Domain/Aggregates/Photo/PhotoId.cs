using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTelegram.Domain.Aggregates.Photo;

public class PhotoId : Identity<PhotoId>
{
    public PhotoId(string value) : base(value)
    {
    }

    public static PhotoId Create(long serverFileId)
    {
        return NewDeterministic(GuidFactories.Deterministic.Namespaces.Commands, $"photoid-{serverFileId}");
    }
}