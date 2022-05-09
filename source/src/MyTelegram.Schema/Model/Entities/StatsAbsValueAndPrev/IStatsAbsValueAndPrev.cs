// ReSharper disable All

namespace MyTelegram.Schema;

public interface IStatsAbsValueAndPrev : IObject
{
    double Current { get; set; }
    double Previous { get; set; }

}
