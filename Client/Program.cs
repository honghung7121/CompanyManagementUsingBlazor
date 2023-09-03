using AutoMapper;
using Blazored.Toast;
using Client;
using Client.Helpers;
using Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
var mapperConfiguration = new MapperConfiguration(configuration =>
{
    configuration.AddProfile(new MappingProfile());
});
builder.Services.AddSingleton(mapperConfiguration.CreateMapper()); 

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5225") });
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddBlazorBootstrap();
builder.Services.AddBlazoredToast();
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();
