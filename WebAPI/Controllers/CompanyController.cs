using BusinessObjects.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;
        public CompanyController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            try
            {
                var companies = await _companyRepository.GetCompanies();
                return Ok(companies.ToList());
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("{id}", Name = "GetCompany")]
        public async Task<IActionResult> GetCompany(int id)
        {
            try
            {
                var company = await _companyRepository.GetCompany(id);
                if (company == null)
                    return NotFound();
                return Ok(company);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
        /*
         * eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQWRtaW4iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbiIsImV4cCI6MTY5MjcwMjUwNH0.sQYjDstqSlranhUjqCe4xYhu1vFQK2KN-LdOWF45wJn-zswlTTOy9_Tm7pCw8ip29wihVGD_iBU-cjVSdDvJ1Q
         */
        [Authorize(Roles="Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateCompany(CompanyDto company)  
        {
            try
            {
                var createdCompany = await _companyRepository.CreateCompany(company);
                return CreatedAtAction("GetCompany", new {id = createdCompany.Id}, createdCompany);            
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(int id, CompanyDto company)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest();

                var dbCompany = await _companyRepository.GetCompany(id);
                if (dbCompany == null)
                    return NotFound();
                await _companyRepository.UpdateCompany(id, company);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            try
            {
                var dbCompany = await _companyRepository.GetCompany(id);
                if (dbCompany == null)
                    return NotFound();
                await _companyRepository.DeleteCompany(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("{id}/MultipleResult")]
        public async Task<IActionResult> GetCompanyEmployeesMultipleResult(int id)
        {
            try
            {
                var company = await _companyRepository.GetCompanyEmployeesMultipleResults(id);
                if (company == null)
                    return NotFound();
                return Ok(company);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("ByEmployeeId/{id}")]
        public async Task<IActionResult> GetCompanyForEmployee(int id)
        {
            try
            {
                var company = await _companyRepository.GetCompanyByEmployeeId(id);
                if (company == null)
                    return NotFound();
                return Ok(company);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

    }
}
