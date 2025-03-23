using Domain.Model;
using Domain.Repository;
using Domain.Service;

namespace Infrastructure.Service
{
    public class BillItemService : IBillItemService
    {
        private readonly IBillItemRepository _billItemRepository;
        private readonly IBillService _billService;
        private readonly IProductService _productService;

        public BillItemService(IBillItemRepository billItemRepository, IBillService billService, IProductService productService)
        {
            _billItemRepository = billItemRepository;
            _billService = billService;
            _productService = productService;
        }

        private double CalculateBillItemPrice(BillItem billItem)
        {
            Product product = _productService.GetById(billItem.ProductId)!;
            return product.Price * billItem.Amount;
        }

        public BillItem Create(BillItem billItem)
        {
            billItem.Price = CalculateBillItemPrice(billItem);
            Bill bill = _billService.GetById(billItem.BillId)!;
            _billService.UpdateTotalPrice(bill, billItem.Price);
            return _billItemRepository.Create(billItem);
        }

        public IEnumerable<BillItem> GetAllByBill(Guid billId)
        {
            return GetAll().Where(x => x.BillId == billId);
        }

        private double CalculateBillItemsPrice(IEnumerable<BillItem> billItems)
        {
            return billItems.Sum(x => x.Price);
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
