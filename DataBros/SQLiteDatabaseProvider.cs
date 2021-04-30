using System.Data;
using System.Data.SQLite;

namespace DataBros
{
    public class SQLiteDatabaseProvider : IDatabaseProvider
    {
        private readonly string connectionString;

        public SQLiteDatabaseProvider(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            return new SQLiteConnection(connectionString);
        }

    }
}
