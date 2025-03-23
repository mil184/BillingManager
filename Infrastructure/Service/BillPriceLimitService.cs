using Domain.Model;
using Domain.Repository;

namespace Domain.Service
{
    public class BillPriceLimitService : IBillPriceLimitService
    {
        private readonly IBillPriceLimitRepository _billPriceLimitRepository;

        public BillPriceLimitService(IBillPriceLimitRepository billPriceLimitRepository)
        {
            _billPriceLimitRepository = billPriceLimitRepository;
        }

        public BillPriceLimit GetNewest()
        {
            return GetAll().OrderByDescending(x => x.DateAdded).FirstOrDefault();
        }

        public BillPriceLimit Create(BillPriceLimit billPriceLimit)
        {
            return _billPriceLimitRepository.Create(billPriceLimit);
        }

        public void Delete(Guid id)
        {
            var billPriceLimit = _billPriceLimitRepository.GetById(id);
            if (billPriceLimit != null)
            {
                _billPriceLimitRepository.Delete(billPriceLimit);
            }
        }

        public IEnumerable<BillPriceLimit> GetAll()
        {
            return _billPriceLimitRepository.GetAll();
        }

        public BillPriceLimit? GetById(Guid id)
        {
            return _billPriceLimitRepository.GetById(id);
        }

        public void Update(BillPriceLimit billPriceLimit)
        {
            _billPriceLimitRepository.Update(billPriceLimit);
        }
    }
}
