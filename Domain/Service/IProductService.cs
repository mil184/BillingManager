using Domain.Model;

namespace Domain.Service
{
    public interface IProductService
    {
        Product Create(Product product);
        void Delete(Guid id);
        IEnumerable<Product> GetAll();
        Product? GetById(Guid id);
        void Update(Product product);
    }
}
