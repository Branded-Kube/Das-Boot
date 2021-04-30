using System.Collections.Generic;
using System.Data;

namespace DataBros
{
    public class Mapper : IMapper
    {
        
        public List<Water> MapWaterFromReader(IDataReader reader)
        {
            var result = new List<Water>();
            while (reader.Read())
            {
                var name = reader.GetString(1);
                var size = reader.GetInt32(2);
                var id = reader.GetInt32(0);
                var type = reader.GetBoolean(3);
                result.Add(new Water() { Id = id, Name = name, Size = size , Type = type});
            }
            return result;
        }
        public List<Bait> MapBaitFromReader(IDataReader reader)
        {
            var result1 = new List<Bait>();
            while (reader.Read())
            {
                var id = reader.GetInt32(0);
                var biteTime = reader.GetInt32(1);
                var price = reader.GetInt32(2);
                var name = reader.GetString(3);
                var alive = reader.GetBoolean(4);

                result1.Add(new Bait() { Id = id, BaitName = name, Price = price, BiteTime = biteTime, Alive = alive});
            }
            return result1;
        }
        public List<Player> MapPlayerFromReader(IDataReader reader)
        {
            var result1 = new List<Player>();
            while (reader.Read())
            {
                var id = reader.GetInt32(0);
                var name = reader.GetString(1);
                var money = reader.GetInt32(2);
                var password = reader.GetString(3);

                result1.Add(new Player() { Id = id, Name = name, Money = money, Password = password });
            }
            return result1;
        }

        public List<Fish> MapFishFromReader(IDataReader reader)
        {
            var result = new List<Fish>();
            while (reader.Read())
            {
                var id = reader.GetInt32(0);
                var name = reader.GetString(1);
                var weight = reader.GetInt32(2);
                var price = reader.GetInt32(3);
                var WaterFk = reader.GetInt32(4);
                var strenght = reader.GetInt32(5);

                result.Add(new Fish() { Id = id, Name = name, Weight = weight, Price = price, WaterFK = WaterFk, Strenght= strenght });
            }

            return result;
        }
    }
}
