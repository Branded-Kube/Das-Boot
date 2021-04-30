using System.Data;

namespace DataBros
{
    public interface IDatabaseProvider
    {
        IDbConnection CreateConnection();

    }
}
