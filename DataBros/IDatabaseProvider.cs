using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataBros
{
    public interface IDatabaseProvider
    {
        IDbConnection CreateConnection();

    }
}
