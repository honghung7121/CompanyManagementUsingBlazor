using BusinessObjects;
using BusinessObjects.Dto;
using DataAccessObjects;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly CompanyDAO _companyDAO;
        public CompanyRepository(CompanyDAO companyDAO)
        {
            _companyDAO = companyDAO;
        }
        
        public Task<Company> CreateCompany(CompanyDto company)
        {
            return _companyDAO.CreateCompany(company);
        }

        public Task DeleteCompany(int id)
        {
            return _companyDAO.DeleteCompany(id);
        }

        public Task<IEnumerable<Company>> GetCompanies()
        {
            return _companyDAO.GetCompanies();
        }

        public Task<Company> GetCompany(int id)
        {
            return _companyDAO.GetCompany(id);
        }

        public Task<Company> GetCompanyByEmployeeId(int id)
        {
            return _companyDAO.GetCompanyByEmployeeId(id);
        }

        public Task<Company> GetCompanyEmployeesMultipleResults(int id)
        {
            return _companyDAO.GetCompanyEmployeesMultipleResults(id);
        }

        public Task UpdateCompany(int id, CompanyDto company)
        {
            return _companyDAO.UpdateCompany(id, company);
        }
    }
}
