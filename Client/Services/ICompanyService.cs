using BusinessObjects;
using BusinessObjects.Dto;

namespace Client.Services
{
    public interface ICompanyService
    {
        List<Company> Companies { get; set; }
        Task GetCompanies();
        Task<Company?> GetComapyById(int id);
        Task CreateCompany(CompanyDto company);
        Task UpdateCompany(int id, CompanyDto company);
        Task DeleteCompany(int id);
    }
}
