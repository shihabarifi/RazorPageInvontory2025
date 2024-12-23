using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using RazorPageInvontory.Models;
using RazorPageInvontory.Modules.UsersSys.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace RazorPageInvontory.Modules.UsersSys.DAL
{
    public class AuthenticateUserSer
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor httpContextAccessor;
        private const string BaseUrl = "/api/user";
        private string _token;
        public AuthenticateUserSer(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<AuthModel> LoginAsync(UserLoginRequest request)
        {

            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/login", request);

            if (response.IsSuccessStatusCode)
            {
                var authResult = await response.Content.ReadFromJsonAsync<AuthModel>();
                if (authResult != null && authResult.IsAuthenticated)
                {
                    _token = authResult.Token;
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                }
                // var X = await _httpClient.GetFromJsonAsync<string>("http://localhost:5236/api/Auth/refreshToken");
                // var X = await _httpClient.GetFromJsonAsync<string>($"{BaseUrl}/protected");
               // var X = GetCustomersDataAsync();
                return authResult;
            }

            return new AuthModel { Message = "Login failed.", IsAuthenticated = false };
        }

        public async Task<string> GETProtectedAPI()
        {
            var token = httpContextAccessor.HttpContext?.Session.GetString("Token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync("/api/CustomersInfo");

            

            return response.StatusCode.ToString();
        }



    }
}
