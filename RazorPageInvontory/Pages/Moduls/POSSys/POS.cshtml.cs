using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageInvontory.Models;
using RazorPageInvontory.Modules.POSSys.BLL;
using RazorPageInvontory.Modules.POSSys.Models;
using System.Text.Json;

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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
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
        [BindProperty]
        public SalesInvoiceHeader Invoice { get; set; }

        public List<FundUser> Funds { get; set; }
        public List<SalePointUser> SalePoints { get; set; }
        public List<Store> Stores { get; set; }
        public List<CustomerInfo> Customers { get; set; } = new List<CustomerInfo>();
        

        [BindProperty]
        public string PeriodName { get; set; }

        [BindProperty]
        public long CashBoxID { get; set; }

        [BindProperty]
        public long SalePointID { get; set; }

        [BindProperty]
        public long StoreID { get; set; }

        [BindProperty]
        public int InvoiceID { get; set; }

        [BindProperty]
        public DateTime invoiceDate { get; set; }


        [BindProperty]
        public int customerSelect { get; set; }

        [BindProperty]
        public int CustomerNumber { get; set; }

        [BindProperty]
        public string? PaymentMethod { get; set; }


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


        [BindProperty]
        public int CreatedBy { get; set; }

        


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
