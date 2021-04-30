using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace DataBros
{
    public class Repository : IRepository
    {
        private readonly IDatabaseProvider provider;
        private readonly IMapper mapper;
        private IDbConnection connection;

        public Repository(IDatabaseProvider provider, IMapper mapper)
        {
            this.provider = provider;
            this.mapper = mapper;
        }

        private void CreateDatabaseTables()
        {
            var cmd = new SQLiteCommand($"PRAGMA foreign_keys = ON;", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();

            cmd = new SQLiteCommand($"CREATE TABLE IF NOT EXISTS Player (PlayerID INTEGER PRIMARY KEY, Name VARCHAR(50), Money INTEGER, Password VARCHAR(50), UNIQUE(Name));", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
            
            cmd = new SQLiteCommand($"CREATE TABLE IF NOT EXISTS Fish (FishID INTEGER PRIMARY KEY, Name VARCHAR(50), Weight INTEGER, Price INTEGER, WaterFK INTEGER, Strenght INTEGER, FOREIGN KEY (WaterFK) REFERENCES Water(WaterID), UNIQUE(Name));", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();

            cmd = new SQLiteCommand($"CREATE TABLE IF NOT EXISTS Water (WaterID INTEGER PRIMARY KEY, Name VARCHAR(50), Size INTEGER, Type BOOLEAN, UNIQUE(Name));", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();

            cmd = new SQLiteCommand($"CREATE TABLE IF NOT EXISTS Bait (BaitID INTEGER PRIMARY KEY, BiteTimeMultiplier INTEGER, Price INTEGER, Name VARCHAR(50), Alive BOOLEAN, UNIQUE(Name));", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
        }

       

        public Water FindWater(string name)
        {

            var cmd = new SQLiteCommand($"SELECT * from Water WHERE name = '{name}'", (SQLiteConnection)connection);
            var reader = cmd.ExecuteReader();

            var result = mapper.MapWaterFromReader(reader).First();
            return result;
        }

        public void AddWater(string name, int size, bool type)
        {
            var cmd = new SQLiteCommand($"INSERT OR IGNORE INTO Water (Name, Size, Type) VALUES ('{name}', {size}, {type})", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
        }

        public void AddPlayer(string name, int money, string password)
        {
            var cmd = new SQLiteCommand($"INSERT OR IGNORE INTO Player (Name, Money, Password) VALUES ('{name}',{money},'{password}')", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
        }
        public void DelPlayers()
        {
            var cmd = new SQLiteCommand($"DELETE FROM Player", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
        }

        public void UpdatePlayers(string name, int money)
        {
            var cmd = new SQLiteCommand($"UPDATE Player SET Money = {money} WHERE Name = '{name}' ", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
        }

        public Player FindPlayer(string name)
        {

            var cmd = new SQLiteCommand($"SELECT * from Player WHERE name = '{name}'", (SQLiteConnection)connection);
            var reader = cmd.ExecuteReader();

            var result = mapper.MapPlayerFromReader(reader).First();
            return result;
        }

        public List<Fish> FindAFish(int waterId)
        {
            var cmd = new SQLiteCommand($"SELECT * from Fish WHERE WaterFK = '{waterId}'", (SQLiteConnection)connection);

            var reader = cmd.ExecuteReader();
            var result = mapper.MapFishFromReader(reader);
            return result;
        }
        public void AddBait(string name, int price, int biteTime, bool alive)
        {
            var cmd = new SQLiteCommand($"INSERT OR IGNORE INTO Bait (Name, Price, BiteTimeMultiplier, Alive) VALUES ('{name}', {price}, {biteTime}, {alive})", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
        }

        public Bait FindBait(string BaitName)
        {
            var cmd = new SQLiteCommand($"SELECT * from Bait WHERE name = '{BaitName}'", (SQLiteConnection)connection);
            var reader = cmd.ExecuteReader();

            var result1 = mapper.MapBaitFromReader(reader).First();
            return result1;
        }
        public void AddFish(string name, int weight, int price, int FKID, int strenght)
        {
            var cmd = new SQLiteCommand($"INSERT OR IGNORE INTO Fish (Name,Weight,Price,WaterFK,Strenght) VALUES ('{name}',{weight},{price},{FKID},{strenght})", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
        }

        public void Open()
        {
            if (connection == null)
            {
                connection = provider.CreateConnection();
            }
            connection.Open();

            CreateDatabaseTables();
        }

        public void Close()
        {
            connection.Close();
        }
    }
}
