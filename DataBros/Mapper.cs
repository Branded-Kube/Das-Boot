using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;

namespace DataBros
{
    public class Mapper : IMapper
    {
        public List<Character> MapCharactersFromReader(IDataReader reader)
        {
            var result = new List<Character>();
            while (reader.Read())
            {
                var name = reader.GetString(1);
                var xp = reader.GetInt32(2);
                var id = reader.GetInt32(0);

                // var id = reader.GetInt32(0);
                // var BaitName = reader.GetString(1);
                // var Cost = reader.GetInt32(2);

                result.Add(new Character() { Id = id, Name = name, Experience = xp });

                // result.Add(new Character() { Id = id, Name = BaitName, Experience = Cost });

            }

            return result;
        }
        public List<Water> MapWaterFromReader(IDataReader reader)
        {
            var result = new List<Water>();
            while (reader.Read())
            {
                var name = reader.GetString(1);
                var size = reader.GetInt32(2);
                var id = reader.GetInt32(0);
                var type = reader.GetBoolean(3);


                // var id = reader.GetInt32(0);
                // var BaitName = reader.GetString(1);
                // var Cost = reader.GetInt32(2);

                result.Add(new Water() { Id = id, Name = name, Size = size , Type = type});

                // result.Add(new Character() { Id = id, Name = BaitName, Experience = Cost });

            }

            return result;
        }
        public List<Bait> MapBaitFromReader(IDataReader reader)
        {
            var result1 = new List<Bait>();
            while (reader.Read())
            {
                var id = reader.GetInt32(0);
                var name = reader.GetString(3);
                var Cost = reader.GetInt32(2);

                // var id = reader.GetInt32(0);
                // var BaitName = reader.GetString(1);
                // var Cost = reader.GetInt32(2);

                result1.Add(new Bait() { Id = id, BaitName = name, Price = Cost });

                // result.Add(new Character() { Id = id, Name = BaitName, Experience = Cost });

            }
            return result1;
        }

        public List<Fish> MapFishFromReader(IDataReader reader)
        {
            var result = new List<Fish>();
            while (reader.Read())
            {
                var name = reader.GetString(1);
                var price = reader.GetInt32(5);
                var id = reader.GetInt32(0);

                result.Add(new Fish() { Id = id, Name = name, Price = price});
            }

            return result;
        }
    }
}
