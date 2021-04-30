using System.Collections.Generic;
using System.Data;

namespace DataBros
{
    public interface IMapper
    {
        List<Water> MapWaterFromReader(IDataReader reader);

        List<Bait> MapBaitFromReader(IDataReader reader);
        List<Fish> MapFishFromReader(IDataReader reader);
        List<Player> MapPlayerFromReader(IDataReader reader);

    }
}
