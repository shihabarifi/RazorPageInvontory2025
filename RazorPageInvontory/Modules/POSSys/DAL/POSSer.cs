using RazorPageInvontory.Models;
using RazorPageInvontory.Modules.POSSys.Models;
using System.Net.Http;
using System.Text.Json;

namespace RazorPageInvontory.Modules.POSSys.DAL
{
    public class POSSer
    {
        private readonly HttpClient _httpClient;

        public POSSer(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        /// <summary>
        /// API جلب بيانات العملاء من
        /// </summary>
        /// <returns>قائمة باسماء العملاء</returns>
        public async Task<List<CustomerInfo>> GetCustomersDataAsync()
        {
            try
            {
                //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var customers = await _httpClient.GetFromJsonAsync<List<CustomerInfo>>("/api/CustomersInfo");

                return customers??new List<CustomerInfo>(); // إرجاع قائمة العملاء مباشرةً
            }
            catch (HttpRequestException ex)
            {
                // معالجة أخطاء HTTP (مثل مشاكل الاتصال أو رموز حالة خطأ)
                Console.WriteLine($"HTTP Request Error: {ex.Message}");
                // يمكنك هنا فحص ex.StatusCode للحصول على رمز حالة HTTP
                return new List<CustomerInfo>(); // أو رمي استثناء إذا كنت تفضل ذلك
            }
           
            catch (Exception ex)
            {
                // معالجة أي أخطاء أخرى
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<CustomerInfo>();
            }
        }

        /// <summary>
        /// API جلب بيانات الصناديق من
        /// </summary>
        /// <returns>قائمة باسماء الصناديق</returns>
        public async Task<List<FundUser>> GetFundUserDataAsync()
        {
            try
            {
                //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var funds = await _httpClient.GetFromJsonAsync<List<FundUser>>("/api/FundsUsers");

                return funds ?? new List<FundUser>(); // إرجاع قائمة العملاء مباشرةً
            }
            catch (HttpRequestException ex)
            {
                // معالجة أخطاء HTTP (مثل مشاكل الاتصال أو رموز حالة خطأ)
                Console.WriteLine($"HTTP Request Error: {ex.Message}");
                // يمكنك هنا فحص ex.StatusCode للحصول على رمز حالة HTTP
                return new List<FundUser>(); // أو رمي استثناء إذا كنت تفضل ذلك
            }

            catch (Exception ex)
            {
                // معالجة أي أخطاء أخرى
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<FundUser>();
            }
        }


        /// <summary>
        /// API جلب بيانات نقاط البيع من
        /// </summary>
        /// <returns>قائمة باسماء نقاط البيع</returns>
        public async Task<List<SalePointUser>> GetSalesPointsAsync()
        {
            try
            {
                //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var pointUsers = await _httpClient.GetFromJsonAsync<List<SalePointUser>>("/api/SalesPoints");

                return pointUsers ?? new List<SalePointUser>(); // إرجاع قائمة العملاء مباشرةً
            }
            catch (HttpRequestException ex)
            {
                // معالجة أخطاء HTTP (مثل مشاكل الاتصال أو رموز حالة خطأ)
                Console.WriteLine($"HTTP Request Error: {ex.Message}");
                // يمكنك هنا فحص ex.StatusCode للحصول على رمز حالة HTTP
                return new List<SalePointUser>(); // أو رمي استثناء إذا كنت تفضل ذلك
            }

            catch (Exception ex)
            {
                // معالجة أي أخطاء أخرى
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<SalePointUser>();
            }
        }

        /// <summary>
        /// API جلب بيانات المخازن من
        /// </summary>
        /// <returns>قائمة باسماء المخازن</returns>
        public async Task<List<Store>> GetStoresAsync()
        {
            try
            {
                //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var stores = await _httpClient.GetFromJsonAsync<List<Store>>("/api/Stores");

                return stores ?? new List<Store>(); // إرجاع قائمة العملاء مباشرةً
            }
            catch (HttpRequestException ex)
            {
                // معالجة أخطاء HTTP (مثل مشاكل الاتصال أو رموز حالة خطأ)
                Console.WriteLine($"HTTP Request Error: {ex.Message}");
                // يمكنك هنا فحص ex.StatusCode للحصول على رمز حالة HTTP
                return new List<Store>(); // أو رمي استثناء إذا كنت تفضل ذلك
            }

            catch (Exception ex)
            {
                // معالجة أي أخطاء أخرى
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<Store>();
            }
        }

        /// <summary>
        /// API جلب بيانات الاصناف من
        /// </summary>
        /// <returns>قائمة باسماء الاصناف</returns>
        public async Task<List<Products>> GetProductsAsync()
        {
            try
            {
                //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var product = await _httpClient.GetFromJsonAsync<List<Products>>("/api/Product");

                return product ?? new List<Products>(); // إرجاع قائمة العملاء مباشرةً
            }
            catch (HttpRequestException ex)
            {
                // معالجة أخطاء HTTP (مثل مشاكل الاتصال أو رموز حالة خطأ)
                Console.WriteLine($"HTTP Request Error: {ex.Message}");
                // يمكنك هنا فحص ex.StatusCode للحصول على رمز حالة HTTP
                return new List<Products>(); // أو رمي استثناء إذا كنت تفضل ذلك
            }

            catch (Exception ex)
            {
                // معالجة أي أخطاء أخرى
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<Products>();
            }
        }

        /// <summary>
        /// API جلب بيانات الاصناف من
        /// </summary>
        /// <returns>قائمة باسماء الاصناف</returns>
        public async Task<List<Unit>> GetUnitsByClassIdDataAsync(long classId)
        {
            try
            {
                //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
               // var product = await _httpClient.GetFromJsonAsync<List<Unit>>("/api/Units");
                var units = await _httpClient.GetFromJsonAsync<List<Unit>>($"/api/Units/{classId}"); // تمرير ClassID في URL


                return units ?? new List<Unit>(); // إرجاع قائمة العملاء مباشرةً
            }
            catch (HttpRequestException ex)
            {
                // معالجة أخطاء HTTP (مثل مشاكل الاتصال أو رموز حالة خطأ)
                Console.WriteLine($"HTTP Request Error: {ex.Message}");
                // يمكنك هنا فحص ex.StatusCode للحصول على رمز حالة HTTP
                return new List<Unit>(); // أو رمي استثناء إذا كنت تفضل ذلك
            }

            catch (Exception ex)
            {
                // معالجة أي أخطاء أخرى
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<Unit>();
            }
        }
    }
}
