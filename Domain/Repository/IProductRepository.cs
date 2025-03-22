using Domain.Model;

namespace Domain.Repository
{
    public interface IProductRepository
    {
        Product Create(Product product);
        void Delete(Product product);
        IEnumerable<Product> GetAll();
        Product? GetById(Guid id);
        void Update(Product product);
    }
}
