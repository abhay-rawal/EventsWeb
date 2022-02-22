using Events_Repository.Repository.IRepository;
using EventsWeb.Shared.Model;

namespace EventsWeb.Server.ProductController
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<EventsProduct> Create(EventsProduct objDTO)
        {
            return _productRepository.Create(objDTO);  
        }

        public Task<int> Delete(int id)
        {
           return _productRepository.Delete(id);
        }

        public Task<EventsProduct> Get(int id)
        {
            return _productRepository.Get(id);
        }

        public Task<IEnumerable<EventsProduct>> GetAll()
        {
            return _productRepository.GetAll();
        }

        public Task<EventsProduct> Update(EventsProduct objDTO)
        {
            return _productRepository.Update(objDTO);
        }
    }
}
