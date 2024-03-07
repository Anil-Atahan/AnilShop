using System.Data;

namespace AnilShop.Reporting.Abstractions.Data;

public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}