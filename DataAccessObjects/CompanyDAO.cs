using BusinessObjects;
using BusinessObjects.Dto;
using Dapper;
using System.Data;

namespace DataAccessObjects
{
    public class CompanyDAO
    {
        private readonly DbContext _context;
        public CompanyDAO(DbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Company>> GetCompanies()
        {
            var query = "SELECT * FROM Company";
            using (var connection = _context.CreateConnection())
            {
                var companies = await connection.QueryAsync<Company>(query);
                return companies;
            }
        }
     
        public async Task<Company> GetCompany(int id)
        {
            var query = "SELECT * FROM Company WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                var company = await connection.QuerySingleOrDefaultAsync<Company>(query, new { id });
                return company;
            }
        }
        public async Task<Company> CreateCompany(CompanyDto company)
        {
            var query = "INSERT INTO Company (Name, Address, Country) VALUES (@Name, @Address, @Country)" +
        "SELECT CAST(SCOPE_IDENTITY() as int)";
            var parameters = new DynamicParameters();
            parameters.Add("Name", company.Name, DbType.String);
            parameters.Add("Address", company.Address, DbType.String);
            parameters.Add("Country", company.Country, DbType.String);
            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);
                var createdCompany = new Company
                {
                    Id = id,
                    Name = company.Name,
                    Address = company.Address,
                    Country = company.Country
                };
                return createdCompany;
            }
        }
        public async Task UpdateCompany(int id, CompanyDto company)
        {
            var query = "UPDATE Company SET Name = @Name, Address = @Address, Country = @Country WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("Name", company.Name, DbType.String);
            parameters.Add("Address", company.Address, DbType.String);
            parameters.Add("Country", company.Country, DbType.String);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task DeleteCompany(int id)
        {
            var query = "DELETE FROM Company WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }
        public async Task<Company> GetCompanyEmployeesMultipleResults(int id)
        {
            var query = "SELECT * FROM Company WHERE Id = @Id;" +
                        "SELECT * FROM Employee WHERE CompanyId = @Id";
            using (var connection = _context.CreateConnection())
            using (var multi = await connection.QueryMultipleAsync(query, new { id }))
            {
                var company = await multi.ReadSingleOrDefaultAsync<Company>();
                if (company != null)
                    company.Employees = (await multi.ReadAsync<Employee>()).ToList();
                return company;
            }
        }
        public async Task<Company> GetCompanyByEmployeeId(int id)
        {
            var procedureName = "ShowCompanyForProvidedEmployeeId";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var company = await connection.QueryFirstOrDefaultAsync<Company>
                    (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return company;
            }
        }
    }
}