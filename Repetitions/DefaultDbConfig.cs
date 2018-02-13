using System.Data.Entity;
using Npgsql;

namespace Repetitions
{
    public class DefaultDbConfig : DbConfiguration
    {
        public DefaultDbConfig()
        {
            SetProviderServices("Npgsql", NpgsqlServices.Instance);
            SetProviderFactory("Npgsql", NpgsqlFactory.Instance);
            SetDefaultConnectionFactory(new NpgsqlConnectionFactory());
        }
    }
}