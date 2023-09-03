using BusinessObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IEmployeeRepository
    {
        public Task<IEnumerable<Employee>> GetEmployees();
        public Employee GetEmployee(int id);
    }
}
