using Domain.Model;
using Domain.Repository;
using Domain.Service;

namespace Infrastructure.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Product Create(Product product) => _productRepository.Create(product);

        public void Delete(Guid id)
        {
            var product = _productRepository.GetById(id);
            if (product != null)
                _productRepository.Delete(product);
        }

        public IEnumerable<Product> GetAll() => _productRepository.GetAll();

        public Product? GetById(Guid id) => _productRepository.GetById(id);

        public void Update(Product product) => _productRepository.Update(product);
    }
}
