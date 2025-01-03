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
            // ������� ������ ������� ��������� �������� ����� �����
            Customers = await _bLLShared.CustomerInfoAsync();
            Funds = await _bLLShared.FundUserAsync();
            SalePoints = await _posManager.SalesPointsAsync();
            Stores = await _bLLShared.StoreAsync();
          

            if (invoiceId.HasValue)
            {
                Products = _bLLShared.ProductsAsync().Result;

                // ��� �� ����� ���� �����ɡ ��� ��� ��������
                
                sPSellInvoice = await _posManager.GetInvoiceByIdAsync(invoiceId.Value);
                TotalAmount = sPSellInvoice.InvoiceDetails.Sum(x => x.TotalAMount);
                DiscountAmount = sPSellInvoice.Descount;
                paidAmoint = sPSellInvoice.PayAmount;
                if (sPSellInvoice == null)
                {
                    // �� ��� �� ��� ������ ��� �������ɡ ��� ����� ����� �������� �� ��� ����� ���
                    RedirectToPage("/NotFound");
                }
            }
            else
            {
                sPSellInvoice.TheNumber = "15";

            }
           
        }

        public async Task<IActionResult> OnGetInvoiceJsonAsync(int invoiceId)
        {
                         
            var invoice = await _posManager.GetInvoiceByIdAsync(invoiceId);
            var inv = invoice.InvoiceDetails;
           
            if (invoice == null)
            {
                return NotFound("Invoice not found.");
            }

            return new JsonResult(inv); // ����� ������ �������� ��� JSON
        }


        #region ���� ��������
        //��� �������
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

        //��� �������
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

        //��� ��������
        public async Task<IActionResult> OnPostSaveAsync([FromBody] SPSellInvoice sPSellInvoice)
        {
            if (true)
            {//�� ������� 
                if (sPSellInvoice.ID > 0)
                {
                    var Editresult = _posManager.SendInvoiceForEditToApiAsync(sPSellInvoice).Result;
                    if (Editresult.Success)
                    {
                        return new JsonResult(new { Editresult.Success, Editresult.Message });
                    }
                    else
                    {
                        Editresult.Message = "��� ��� ����� ��� ��������: ";
                        return new JsonResult(new { Editresult.Success, Editresult.Message });
                    }
                }
                var result = _posManager.SendInvoiceToApiAsync(sPSellInvoice).Result;
                if (result.Success)
                {
                    return new JsonResult(new { result.Success, result.Message });
                }
                else
                {
                    result.Message = "��� ��� ����� ��� ��������: ";
                    return new JsonResult(new { result.Success, result.Message });
                }
            }
        }
        #endregion

        #region ���������


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
