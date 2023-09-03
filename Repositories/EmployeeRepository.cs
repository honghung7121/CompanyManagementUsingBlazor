using BusinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDAO _employeeDAO;
        public EmployeeRepository(EmployeeDAO companyDAO)
        {
            _employeeDAO = companyDAO;
        }

        public Employee GetEmployee(int id)
        {
           return _employeeDAO.GetAllEmployee(id);
        }

        public Task<IEnumerable<Employee>> GetEmployees()
        {
            return _employeeDAO.GetEmployees();
        }
    }
}
