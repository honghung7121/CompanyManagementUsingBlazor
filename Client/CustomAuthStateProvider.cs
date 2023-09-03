using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;

namespace Client
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _http;
        public CustomAuthStateProvider(ILocalStorageService localStorage, HttpClient http)
        {
            _localStorage = localStorage;
            _http = http;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string token = await _localStorage.GetItemAsStringAsync("jwtToken");
            //string token = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQWRtaW4iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbiIsImV4cCI6MTY5MzIxNzg1OH0.5Ro7V7DrXTmJagiefdrReRP7WrZZ8JO7QTv4ZI3Hu1rFpJ8klukpGODnSXoDQ_mA_8lTtlOtzGMuPfL0BD7rkw";
            if (!string.IsNullOrEmpty(token))
            {
                var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
                var user = new ClaimsPrincipal(identity);
                var state = new AuthenticationState(user);
                NotifyAuthenticationStateChanged(Task.FromResult(state));
                return state;
            }
            else
            {
                var state = new AuthenticationState(new ClaimsPrincipal());
                NotifyAuthenticationStateChanged(Task.FromResult(state));
                return state;
            }
        }
        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
