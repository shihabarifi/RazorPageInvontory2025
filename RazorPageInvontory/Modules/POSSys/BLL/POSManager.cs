using RazorPageInvontory.Models;
using RazorPageInvontory.Modules.POSSys.DAL;
using RazorPageInvontory.Modules.POSSys.Models;
using RazorPageInvontory.Modules.UsersSys.Models;

namespace RazorPageInvontory.Modules.POSSys.BLL
{
    public class POSManager
    {
        private readonly POSSer _posService;

        public POSManager(POSSer posService)
        {
            _posService = posService;
        }
        public async Task<List<CustomerInfo>> CustomerInfoAsync()
        {

            return await _posService.GetCustomersDataAsync();
        }

        public async Task<List<FundUser>> FundUserAsync()
        {

            return await _posService.GetFundUserDataAsync();
        }

        public async Task<List<SalePointUser>> SalesPointsAsync()
        {

            return await _posService.GetSalesPointsAsync();
        }
        public async Task<List<Store>> StoreAsync()
        {

            return await _posService.GetStoresAsync();
        }

        public async Task<List<Products>> ProductsAsync()
        {

            return await _posService.GetProductsAsync();
        }
        public async Task<List<Unit>> UnitsAsync(long ClassID)
        {

            return await _posService.GetUnitsByClassIdDataAsync(ClassID);
        }
    }
}
