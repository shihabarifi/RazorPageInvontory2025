using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageInvontory.Models;
using RazorPageInvontory.Modules.POSSys.BLL;
using RazorPageInvontory.Modules.POSSys.Models;
using System.Text.Json;
using static RazorPageInvontory.Pages.EmployeeModel;
using static RazorPageInvontory.Pages.Moduls.POSSys.POSModel;

namespace RazorPageInvontory.Pages.Moduls.POSSys
{
    public class POSModel : PageModel
    {
        private readonly POSManager _posManager;
      //  public List<CustomerInfo> customers { get; set; }=new List<CustomerInfo>();
        public POSModel(POSManager posManager)
        {
            _posManager = posManager;
        }
        public async Task OnGet()
        {
            Customers = await _posManager.CustomerInfoAsync();
            Funds = await _posManager.FundUserAsync();
            SalePoints=await _posManager.SalesPointsAsync();
            Stores = await _posManager.StoreAsync();
        }
        public class Invoices
        {
            public string? PeriodNumber { get; set; }
            public int SalePointID { get; set; }
            public string? TheNumber { get; set; }
            public DateTime TheDate { get; set; }
            public string? ThePay { get; set; }
            public int StoreID { get; set; }
            public int AccountID { get; set; }
            public string? CustomerName { get; set; }
            public string? Notes { get; set; }
            public int UserID { get; set; }
            public decimal Descount { get; set; }
            public decimal Debited { get; set; }
            public decimal PayAmount { get; set; }
        }

        [BindProperty]
        public SPSellInvoice sPSellInvoice { get; set; } = new SPSellInvoice();
        [BindProperty]
        public Invoices Invoice { get; set; } = new Invoices();

        public async Task<IActionResult> OnPostSaveAsync([FromBody] SPSellInvoice sPSellInvoice)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

           

            if (sPSellInvoice == null || sPSellInvoice.InvoiceDetails == null || !sPSellInvoice.InvoiceDetails.Any())
            {
                var result = _posManager.SendInvoiceToApiAsync(sPSellInvoice).Result;
                if (result.Success)
                {
                    string message = " „ Õ›Ÿ «·›« Ê—… »‰Ã«Õ.";
                    return new JsonResult(new { message });
                   // return new JsonResult(new { success = true, message = " „ Õ›Ÿ «·›« Ê—… »‰Ã«Õ." });
                }
                else
                {
                    string message = "ÕœÀ Œÿ√ √À‰«¡ Õ›Ÿ «·›« Ê—…: ";
                    return new JsonResult(new { message });

                   
                }
            }
           
            return Page();

        }

     

        #region œÊ«· «·⁄„·Ì« 
        //Ã·» «·«’‰«›
        public JsonResult OnGetGetProducts()
        {
            List<Products> products = new List<Products>();

            try
            {
                products=_posManager.ProductsAsync().Result;

                return new JsonResult(products);
            }
            catch (System.Exception ex)
            {
                return new JsonResult(new { error = ex.Message });
            }
        }

        //Ã·» «·ÊÕœ« 
        public JsonResult OnGetGetUnits(long ProductId)
        {
            List<Unit> units = new List<Unit>();

            try
            {
                units = _posManager.UnitsAsync(ProductId).Result;

                return new JsonResult(units);
            }
            catch (System.Exception ex)
            {
                return new JsonResult(new { error = ex.Message });
            }
        }
        #endregion

        #region «·„ €Ì—« 
 

        public List<FundUser> Funds { get; set; }
        public List<SalePointUser> SalePoints { get; set; }
        public List<Store> Stores { get; set; }
        public List<CustomerInfo> Customers { get; set; } = new List<CustomerInfo>();
        

        
        [BindProperty]
        public decimal TotalAmount { get; set; }

        [BindProperty]
        public decimal Discount { get; set; }

        [BindProperty]
        public decimal DiscountAmount { get; set; }


        [BindProperty]
        public decimal NetAmount { get; set; }

        [BindProperty]
        public string? invoiceDetails { get; set; }


        [BindProperty]
        public decimal paidAmoint { get; set; }




        public List<POSModel> products { get; set; }

        [BindProperty]
        public int ProductID { get; set; }

        [BindProperty]
        public string? UnitName { get; set; }

        [BindProperty]
        public decimal UnitPrice { get; set; }

        [BindProperty]
        public decimal Quantity { get; set; }

        [BindProperty]
        public decimal ItemDescountRatio { get; set; }

        [BindProperty]
        public decimal ItemDescountAmount { get; set; }

        [BindProperty]
        public decimal SubTotal { get; set; }
        #endregion
    }
}
