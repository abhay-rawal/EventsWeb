using EventsWeb.Shared.Model;

namespace EventsWeb.Server.CategoryController
{
    public interface ICategoryService
    {
        public Task<EventsCategory> Create(EventsCategory objDTO);
        public Task<EventsCategory> Update(EventsCategory objDTO);
        public Task<int> Delete(int id);
        public Task<EventsCategory> Get(int id);
        public Task<IEnumerable<EventsCategory>> GetAll();
    }
}
