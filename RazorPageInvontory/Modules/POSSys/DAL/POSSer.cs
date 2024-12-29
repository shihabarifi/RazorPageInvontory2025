using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RazorPageInvontory.Models;
using RazorPageInvontory.Modules.POSSys.Models;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RazorPageInvontory.Modules.POSSys.DAL
{
    public class POSSer
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor httpContextAccessor;

        public POSSer(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            this.httpContextAccessor = httpContextAccessor;
        }
        /// <summary>
        /// API جلب بيانات العملاء من
        /// </summary>
        /// <returns>قائمة باسماء العملاء</returns>
        public async Task<List<CustomerInfo>> GetCustomersDataAsync()
        {
            try
            {
                var token = httpContextAccessor.HttpContext?.Session.GetString("Token");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
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
                var token = httpContextAccessor.HttpContext?.Session.GetString("Token");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
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
                var token = httpContextAccessor.HttpContext?.Session.GetString("Token");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
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
                var token = httpContextAccessor.HttpContext?.Session.GetString("Token");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
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
                var token = httpContextAccessor.HttpContext?.Session.GetString("Token");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
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
                var token = httpContextAccessor.HttpContext?.Session.GetString("Token");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

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

        /// <summary>
        /// API  إضافة فاتورة بيع مباشر من
        /// </summary>
        /// <returns>نجاح العملية ام فشلها</returns>
        public async Task<HttpResponseMessage> SendInvoiceToApiAsync(SPSellInvoice invoice)
        {

            try
            {
                var token = httpContextAccessor.HttpContext?.Session.GetString("Token");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.PostAsJsonAsync($"/api/SPSellInvoice", invoice);
                return response;
              
            }
            catch (HttpRequestException ex)
            {
                // معالجة أخطاء HTTP (مثل مشاكل الاتصال أو رموز حالة خطأ)
                Console.WriteLine($"HTTP Request Error: {ex.Message}");
                // يمكنك هنا فحص ex.StatusCode للحصول على رمز حالة HTTP
                return new HttpResponseMessage(HttpStatusCode.InternalServerError); // أو رمي استثناء إذا كنت تفضل ذلك
            }

            catch (Exception ex)
            {
                // معالجة أي أخطاء أخرى
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }   
        }

        /// <summary>
        /// API  تعديل فاتورة بيع مباشر من
        /// </summary>
        /// <returns>نجاح العملية ام فشلها</returns>
        public async Task<HttpResponseMessage> SendInvoiceForEditToApiAsync(SPSellInvoice invoice)
        {

            try
            {
                var token = httpContextAccessor.HttpContext?.Session.GetString("Token");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.PutAsJsonAsync($"/api/SPSellInvoice/{invoice.ID}", invoice);
                return response;

            }
            catch (HttpRequestException ex)
            {
                // معالجة أخطاء HTTP (مثل مشاكل الاتصال أو رموز حالة خطأ)
                Console.WriteLine($"HTTP Request Error: {ex.Message}");
                // يمكنك هنا فحص ex.StatusCode للحصول على رمز حالة HTTP
                return new HttpResponseMessage(HttpStatusCode.InternalServerError); // أو رمي استثناء إذا كنت تفضل ذلك
            }

            catch (Exception ex)
            {
                // معالجة أي أخطاء أخرى
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// API  تعديل فاتورة بيع مباشر من
        /// </summary>
        /// <returns>نجاح العملية ام فشلها</returns>
        public async Task<HttpResponseMessage> DeleteInvoiceAsync(int invoiceid)
        {

            try
            {
                var token = httpContextAccessor.HttpContext?.Session.GetString("Token");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.DeleteAsync($"/api/SPSellInvoice/{invoiceid}");
                return response;

            }
            catch (HttpRequestException ex)
            {
                // معالجة أخطاء HTTP (مثل مشاكل الاتصال أو رموز حالة خطأ)
                Console.WriteLine($"HTTP Request Error: {ex.Message}");
                // يمكنك هنا فحص ex.StatusCode للحصول على رمز حالة HTTP
                return new HttpResponseMessage(HttpStatusCode.InternalServerError); // أو رمي استثناء إذا كنت تفضل ذلك
            }

            catch (Exception ex)
            {
                // معالجة أي أخطاء أخرى
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// API جلب بيانات الفاتورة من
        /// </summary>
        /// <returns>قائمة باسماء الاصناف</returns>
        public async Task<SPSellInvoice> GetInvoiceByIdAsync(int id)
        {
            try
            {
                var sPSellInvoice = await _httpClient.GetFromJsonAsync<SPSellInvoice>($"/api/SPSellInvoice/{id}");
                if (sPSellInvoice == null)
                {
                    throw new Exception("الفاتورة غير موجودة.");
                }

                return sPSellInvoice;
            }
            catch (HttpRequestException ex)
            {
                // معالجة الأخطاء المتعلقة بالشبكة أو الخادم
                throw new Exception($"خطأ في الاتصال بـ API: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // معالجة الأخطاء العامة
                throw new Exception($"حدث خطأ أثناء جلب الفاتورة: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// API جلب بيانات الاصناف من
        /// </summary>
        /// <returns>قائمة باسماء الاصناف</returns>
        public async Task<List<HeaderInvoice>> GetInvoiceListAsync()
        {
            try
            {
                var token = httpContextAccessor.HttpContext?.Session.GetString("Token");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var pSellInvoices = await _httpClient.GetFromJsonAsync<List<HeaderInvoice>>("/api/SPSellInvoice");

                return pSellInvoices ?? new List<HeaderInvoice>(); // إرجاع قائمة العملاء مباشرةً
            }
            catch (HttpRequestException ex)
            {
                // معالجة أخطاء HTTP (مثل مشاكل الاتصال أو رموز حالة خطأ)
                Console.WriteLine($"HTTP Request Error: {ex.Message}");
                // يمكنك هنا فحص ex.StatusCode للحصول على رمز حالة HTTP
                return new List<HeaderInvoice>(); // أو رمي استثناء إذا كنت تفضل ذلك
            }

            catch (Exception ex)
            {
                // معالجة أي أخطاء أخرى
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<HeaderInvoice>();
            }
        }


        /// <summary>
        /// API جلب بيانات الاصناف من
        /// </summary>
        /// <returns>قائمة باسماء الاصناف</returns>
        public async Task<List<InvoiceDetail>> GetInvoiceDetailsAsync(int invoiceId)
        {
            try
            {
                var token = httpContextAccessor.HttpContext?.Session.GetString("Token");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
               // var sPSellInvoice = await _httpClient.GetFromJsonAsync<SPSellInvoice>($"/api/SPSellInvoice/{id}");
                var response = await _httpClient.GetFromJsonAsync<List<InvoiceDetail>>($"api/SPSellInvoice/details/{invoiceId}");

                if (response == null)
                {
                    throw new Exception("الفاتورة غير موجودة.");
                }
             
                return response;

            }
            catch (HttpRequestException ex)
            {
                // معالجة أخطاء HTTP
                Console.Error.WriteLine($"HTTP Request Error: {ex.Message}");
                throw; // إعادة رمي الاستثناء ليتم التعامل معه في الطبقة الأعلى
            }
            catch (Exception ex)
            {
                // معالجة أخطاء أخرى
                Console.Error.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }




    }
}
