using Domain.Model;
using Domain.Repository;
using Domain.Service;

namespace Infrastructure.Service
{
    public class BillService : IBillService
    {
        private readonly IBillRepository _billRepository;
        private readonly IEmployeeService _employeeService;

        public BillService(IBillRepository billRepository, IEmployeeService employeeService)
        {
            _billRepository = billRepository;
            _employeeService = employeeService;
        }

        public void ProcessBillPayment(Bill bill)
        {
            bill.IsPayed = true;
            Update(bill);
        }

        public void UpdateTotalPrice(Bill bill, double price)
        {
            bill.TotalPrice += price;
            Update(bill);
        }

        public Bill Create(Bill bill)
        {
            //bill.Employee = _employeeService.GetById(bill.EmployeeId);
            return _billRepository.Create(bill);
        }

        public void Delete(Guid id)
        {
            var bill = _billRepository.GetById(id);
            if (bill != null)
                _billRepository.Delete(bill);
        }

        public IEnumerable<Bill> GetAll() => _billRepository.GetAll();

        public Bill? GetById(Guid id) => _billRepository.GetById(id);

        public void Update(Bill bill) => _billRepository.Update(bill);
    }
}
