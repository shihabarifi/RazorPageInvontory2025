using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageInvontory.Models;
using RazorPageInvontory.Modules.POSSys.BLL;
using RazorPageInvontory.Modules.POSSys.Models;
using RazorPageInvontory.Shared.BLLSharedALL;
using System.Text.Json;
using static RazorPageInvontory.Pages.Moduls.POSSys.POSModel;

namespace RazorPageInvontory.Pages.Moduls.POSSys
{
    public class POSModel : PageModel
    {
        private readonly POSManager _posManager;
        private readonly BLLSharedServices _bLLShared;

        //  public List<CustomerInfo> customers { get; set; }=new List<CustomerInfo>();
        public POSModel(POSManager posManager,BLLSharedServices bLLShared)
        {
            _posManager = posManager;
            _bLLShared = bLLShared;
        }
        public async Task OnGetAsync(int? invoiceId = null)
        {
            // «” œ⁄«¡ »Ì«‰«  «·⁄„·«¡ Ê«·’‰«œÌﬁ Ê«·„Œ«“‰ Ê‰ﬁ«ÿ «·»Ì⁄
            Customers = await _bLLShared.CustomerInfoAsync();
            Funds = await _bLLShared.FundUserAsync();
            SalePoints = await _posManager.SalesPointsAsync();
            Stores = await _bLLShared.StoreAsync();


            if (invoiceId.HasValue)
            {
                Products = _bLLShared.ProductsAsync().Result;

                // ≈–«  „  „—Ì— „⁄—› ›« Ê—…° Ì „ Ã·» »Ì«‰« Â«
                sPSellInvoice = await _posManager.GetInvoiceByIdAsync(invoiceId.Value);

                if (sPSellInvoice == null)
                {
                    // ›Ì Õ«· ·„ Ì „ «·⁄ÀÊ— ⁄·Ï «·›« Ê—…° Ì „ ≈⁄«œ…  ÊÃÌÂ «·„” Œœ„ √Ê ⁄—÷ —”«·… Œÿ√
                    RedirectToPage("/NotFound");
                }
            }
           
        }


        #region œÊ«· «·⁄„·Ì« 
        //Ã·» «·«’‰«›
        public JsonResult OnGetGetProducts()
        {
            List<Products> products = new List<Products>();

            try
            {
                products= _bLLShared.ProductsAsync().Result;

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
                units = _bLLShared.UnitsAsync(ProductId).Result;

                return new JsonResult(units);
            }
            catch (System.Exception ex)
            {
                return new JsonResult(new { error = ex.Message });
            }
        }

        //Õ›Ÿ «·›« Ê—…
        public async Task<IActionResult> OnPostSaveAsync([FromBody] SPSellInvoice sPSellInvoice)
        {
            if (true)
            {
                var result = _posManager.SendInvoiceToApiAsync(sPSellInvoice).Result;
                if (result.Success)
                {
                    return new JsonResult(new { result.Message });
                }
                else
                {
                    string message = "ÕœÀ Œÿ√ √À‰«¡ Õ›Ÿ «·›« Ê—…: ";
                    return new JsonResult(new { message });
                }
            }
        }
        #endregion

        #region «·„ €Ì—« 


        [BindProperty]
        public SPSellInvoice sPSellInvoice { get; set; } = new SPSellInvoice();
        public List<FundUser> Funds { get; set; }
        public List<SalePointUser> SalePoints { get; set; }
        public List<Store> Stores { get; set; }
        public List<CustomerInfo> Customers { get; set; } = new List<CustomerInfo>();
        public List<Products> Products { get; set; } = new List<Products>();


        [BindProperty]
        public decimal TotalAmount { get; set; }
        [BindProperty]
        public decimal Discount { get; set; }
        [BindProperty]
        public decimal DiscountAmount { get; set; }
        [BindProperty]
        public decimal NetAmount { get; set; }
        [BindProperty]
        public decimal paidAmoint { get; set; }

        #endregion
    }
}
