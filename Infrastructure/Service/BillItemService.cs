using Domain.Model;
using Domain.Repository;
using Domain.Service;

namespace Infrastructure.Service
{
    public class BillItemService : IBillItemService
    {
        private readonly IBillItemRepository _billItemRepository;
        private readonly IBillService _billService;

        public BillItemService(IBillItemRepository billItemRepository, IBillService billService)
        {
            _billItemRepository = billItemRepository;
            _billService = billService;
        }
        private double CalculateBillItemPrice(BillItem billItem)
        {
            return billItem.Price * billItem.Amount;
        }

        public BillItem Create(BillItem billItem)
        {
            billItem.Price = CalculateBillItemPrice(billItem);

            Bill bill = _billService.GetById(billItem.BillId)!;
            _billService.UpdateTotalPrice(bill, billItem.Price);

            return _billItemRepository.Create(billItem);
        }

        public void Delete(Guid id)
        {
            var billItem = _billItemRepository.GetById(id);
            if (billItem != null)
                _billItemRepository.Delete(billItem);
        }

        public IEnumerable<BillItem> GetAll() => _billItemRepository.GetAll();

        public BillItem? GetById(Guid id) => _billItemRepository.GetById(id);

        public void Update(BillItem billItem) => _billItemRepository.Update(billItem);
    }
}
