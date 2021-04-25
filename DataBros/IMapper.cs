using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;

namespace DataBros
{
    public interface IMapper
    {
        //List<Character> MapCharactersFromReader(SQLiteDataReader reader);
        List<Character> MapCharactersFromReader(IDataReader reader);
        List<Water> MapWaterFromReader(IDataReader reader);


        List<Bait> MapBaitFromReader(IDataReader reader);
        List<Fish> MapFishFromReader(IDataReader reader);


    }
}
