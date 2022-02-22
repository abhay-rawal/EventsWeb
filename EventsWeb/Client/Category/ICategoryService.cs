using EventsWeb.Shared.Model;

namespace EventsWeb.Client.Category
{
    public interface ICategoryService
    {
        public Task<IEnumerable<EventsCategory>> GetAll();
        public Task<EventsCategory> Get(int id);
        Task Create(EventsCategory categoryDTO);
        Task Update(EventsCategory categoryDTO,int id);
        Task<int> Delete(int id);
        Task DeleteImage(string id);
    }
}
