using BusinessObjects;
using BusinessObjects.Dto;

namespace Repositories
{
    public interface ICompanyRepository
    {
        public Task<IEnumerable<Company>> GetCompanies();
        public Task<Company> GetCompany(int id);
        public Task<Company> CreateCompany(CompanyDto company);
        public Task UpdateCompany(int id, CompanyDto company);
        public Task DeleteCompany(int id);
        public Task<Company> GetCompanyEmployeesMultipleResults(int id);
        public Task<Company> GetCompanyByEmployeeId(int id);
    }
}