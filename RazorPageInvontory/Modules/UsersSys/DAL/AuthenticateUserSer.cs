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

        public async Task<List<CustomerInfo>> GetCustomersDataAsync()
        {
            try
            {
                //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var customers = await _httpClient.GetFromJsonAsync<List<CustomerInfo>>("/api/CustomersInfo");

                return customers; // إرجاع قائمة العملاء مباشرةً
            }
            catch (HttpRequestException ex)
            {
                // معالجة أخطاء HTTP (مثل مشاكل الاتصال أو رموز حالة خطأ)
                Console.WriteLine($"HTTP Request Error: {ex.Message}");
                // يمكنك هنا فحص ex.StatusCode للحصول على رمز حالة HTTP
                return null; // أو رمي استثناء إذا كنت تفضل ذلك
            }
            catch (NotSupportedException ex) // إذا كان المحتوى ليس JSON صالحًا
            {
                Console.WriteLine($"Content is not valid JSON: {ex.Message}");
                return null;
            }
            catch (JsonException ex) // خطأ في تحليل JSON
            {
                Console.WriteLine($"JSON parsing error: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                // معالجة أي أخطاء أخرى
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }


    }
}
