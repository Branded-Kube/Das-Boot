using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace DataBros
{
    public class Repository : IAdventurerRepository
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
