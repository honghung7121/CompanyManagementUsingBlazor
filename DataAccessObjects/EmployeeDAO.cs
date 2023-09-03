using BusinessObjects;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class EmployeeDAO
    {
        private readonly DbContext _context;
        public EmployeeDAO(DbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            var query = "SELECT e.*, c.* FROM Employee e INNER JOIN Company c ON e.CompanyId = c.Id";
            using (var connection = _context.CreateConnection())
            {
                var employees = await connection.QueryAsync<Employee, Company, Employee>(query, (employee, company) =>
                {
                    employee.Company = company;
                    return employee;
                });
                return employees;
            }
        }
        
        public Employee GetAllEmployee(int id) 
        {
            var query = "SELECT * from Employee where Id = @Id";
            using(var connection = _context.CreateConnection()) 
            {
                var employee = connection.QueryFirstOrDefault<Employee>(query, new { id });
                return employee;
            }
        }
    }
}
