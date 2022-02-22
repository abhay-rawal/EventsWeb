using EventsWeb.Shared.Model;

namespace EventsWeb.Server.ProductController
{
    public interface IProductService
    {
        public Task<EventsProduct> Create(EventsProduct objDTO);
        public Task<EventsProduct> Update(EventsProduct objDTO);
        public Task<int> Delete(int id);
        public Task<EventsProduct> Get(int id);
        public Task<IEnumerable<EventsProduct>> GetAll();
    }
}
