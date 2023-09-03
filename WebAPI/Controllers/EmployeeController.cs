using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenAI_API;
using OpenAI_API.Completions;
using Repositories;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeController(IEmployeeRepository companyRepository)
        {
            _employeeRepository = companyRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var employess = await _employeeRepository.GetEmployees();
                return Ok(employess);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            try
            {
                var employee = _employeeRepository.GetEmployee(id);
                return Ok(employee);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
        // 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
        [HttpGet("{text}/ChatWithGPT")]
        public async Task<IActionResult> ChatWithGPT(string text)
        {
            string APIKey = "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";
            string respone = string.Empty;

            var openAI = new OpenAIAPI(APIKey);
            CompletionRequest completionRequest = new CompletionRequest();
            completionRequest.Prompt = text;
            completionRequest.Model = OpenAI_API.Models.Model.DavinciText;
            completionRequest.MaxTokens = 200;
            var result = openAI.Completions.CreateCompletionAsync(completionRequest); 
            foreach(var item in result.Result.Completions)
            {
               respone = item.Text; 
            }
            return Ok(respone);
        }
    }
}
