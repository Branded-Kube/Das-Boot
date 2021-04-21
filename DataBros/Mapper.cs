using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;

namespace DataBros
{
    public class AdventurerMapper : IMapper
    {
        public List<Character> MapCharactersFromReader(IDataReader reader)
        {
            var result = new List<Character>();
            while (reader.Read())
            {
                var id = reader.GetInt32(0);
                var name = reader.GetString(1);
                var xp = reader.GetInt32(2);

                result.Add(new Character() { Id = id, Name = name, Experience = xp });
            }
            return result;
        }
    }
}
