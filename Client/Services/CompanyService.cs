using Blazored.Toast.Services;
using BusinessObjects;
using BusinessObjects.Dto;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Net;

namespace Client.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;
        private readonly IToastService _ToastService;
        public CompanyService(HttpClient httpClient, NavigationManager navigationManager, IToastService toastService)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
            _ToastService = toastService;
        }

        public List<Company> Companies { get; set; } = new List<Company>();

        public async Task CreateCompany(CompanyDto company)
        {
            var result = await _httpClient.PostAsJsonAsync($"api/company", company);
            if (result.IsSuccessStatusCode)
                _navigationManager.NavigateTo("/companies");
        }

        public async Task DeleteCompany(int id)
        {
            var result = await _httpClient.DeleteAsync($"api/company/{id}");
            if (result.IsSuccessStatusCode)
            {
                _navigationManager.NavigateTo("/companies");
                _ToastService.ShowSuccess("The Company has been create successfully!");
            }
            else if(result.StatusCode == HttpStatusCode.Unauthorized)
                _ToastService.ShowError("Unauthorization!");

        }

        public async Task<Company?> GetComapyById(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<Company>($"api/company/{id}");
            return result;
        }

        public async Task GetCompanies()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Company>>("api/company");
            if (result is not null)
                Companies = result;
        }

        public async Task UpdateCompany(int id, CompanyDto company)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/company/{id}", company);
            if (result.IsSuccessStatusCode)
                _navigationManager.NavigateTo("/companies");
        }
    }
}
