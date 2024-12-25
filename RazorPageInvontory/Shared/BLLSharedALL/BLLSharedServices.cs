using RazorPageInvontory.Models;
using RazorPageInvontory.Modules.POSSys.DAL;

namespace RazorPageInvontory.Shared.BLLSharedALL
{
    public class BLLSharedServices
    {
        private readonly POSSer _posService;

        public BLLSharedServices(POSSer posService)
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
