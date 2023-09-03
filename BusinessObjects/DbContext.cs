using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BusinessObjects
{
    public class DbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public DbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SqlConnection");
        }
        public DbContext()
        {

        }
        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
    }
}
