using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
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
            var cmd = new SQLiteCommand($"CREATE TABLE IF NOT EXISTS characters (Id INTEGER PRIMARY KEY, Name VARCHAR(50), Experience INTEGER);", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();

            cmd = new SQLiteCommand($"CREATE TABLE IF NOT EXISTS Players (ID INTEGER PRIMARY KEY, Name VARCHAR(50), Money INTEGER, Strengt INTEGER, FOREIGN KEY (ID) REFERENCES Region(ID));", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
            
            cmd = new SQLiteCommand($"CREATE TABLE IF NOT EXISTS Fish (ID INTEGER PRIMARY KEY, Name VARCHAR(50), BiteTime INTEGER, Strengt INTEGER, Weight INTEGER, Price INTEGER);", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();

            cmd = new SQLiteCommand($"CREATE TABLE IF NOT EXISTS Region (ID INTEGER PRIMARY KEY, Name VARCHAR(50), FOREIGN KEY (ID) REFERENCES Water(ID));", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();

            cmd = new SQLiteCommand($"CREATE TABLE IF NOT EXISTS Water (ID INTEGER PRIMARY KEY, Name VARCHAR(50), Size INTEGER, Type BOOLEAN, FOREIGN KEY (ID) REFERENCES Fish(ID));", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();

            cmd = new SQLiteCommand($"CREATE TABLE IF NOT EXISTS Bait (ID INTEGER PRIMARY KEY, BiteTimeMultiplier INTEGER, Price INTEGER, Name VARCHAR(50), Type BOOLEAN);", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
        }

        public void AddCharacter(string name, int experience)
        {
            var cmd = new SQLiteCommand($"INSERT INTO characters (Name, Experience) VALUES ('{name}', {experience})", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
        }


        public Character FindCharacter(string name)
        {
            var cmd = new SQLiteCommand($"SELECT * from characters WHERE name = '{name}'", (SQLiteConnection)connection);
            var reader = cmd.ExecuteReader();

            var result = mapper.MapCharactersFromReader(reader).First();
            return result;
        }

        public List<Character> GetAllCharacters()
        {
            var cmd = new SQLiteCommand("SELECT * from characters", (SQLiteConnection)connection);
            var reader = cmd.ExecuteReader();

            var result = mapper.MapCharactersFromReader(reader);
            return result;
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
            var cmd = new SQLiteCommand($"INSERT INTO Water (Name, Size, Type) VALUES ('{name}', {size}, {type})", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
        }

      
        //public Character FindAFish(string name)
        //{
        //    Random Rnd = new Random();
        //    Rnd.Next(1,4);
        //    Debug.WriteLine(Rnd);
        //    var cmd = new SQLiteCommand($"SELECT * from characters WHERE name = '{name}'", (SQLiteConnection)connection);
        //    var reader = cmd.ExecuteReader();

        //    var result = mapper.MapCharactersFromReader(reader).First();
        //    return result;
        //}
        public void AddBait(string name, int cost)
        {
            var cmd = new SQLiteCommand($"INSERT INTO Bait (Name, Price) VALUES ('{name}', {cost})", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
        }

        public Bait FindBait(string BaitName)
        {
            var cmd = new SQLiteCommand($"SELECT * from Bait WHERE name = '{BaitName}'", (SQLiteConnection)connection);
            var reader = cmd.ExecuteReader();

            var result1 = mapper.MapBaitFromReader(reader).First();
            return result1;
        }

        public List<Bait> GetAllBait()
        {
            var cmd = new SQLiteCommand("SELECT * from bait", (SQLiteConnection)connection);
            var reader = cmd.ExecuteReader();

            var result1 = mapper.MapBaitFromReader(reader);
            return result1;
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
