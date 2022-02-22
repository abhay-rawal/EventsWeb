using EventsWeb.Shared.Model;

namespace EventsWeb.Client.Product
{
    public interface IProductService
    {
        public Task<IEnumerable<EventsProduct>> GetAll();
        public Task<EventsProduct> Get(int id);
        Task Create(EventsProduct objEvents);
        Task Update(EventsProduct objEvents,int id);

        Task Delete(int id);    
    }
}
