using Events_Repository.Repository.IRepository;
using EventsWeb.Shared.Model;

namespace EventsWeb.Server.CategoryController
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Task<EventsCategory> Create(EventsCategory objDTO)
        {
            return _categoryRepository.Create(objDTO);  
        }

        public Task<int> Delete(int id)
        {
           return _categoryRepository.Delete(id);
        }

        public Task<EventsCategory> Get(int id)
        {
            return _categoryRepository.Get(id);
        }

        public Task<IEnumerable<EventsCategory>> GetAll()
        {
            return _categoryRepository.GetAll();
        }

        public Task<EventsCategory> Update(EventsCategory objDTO)
        {
            return _categoryRepository.Update(objDTO);
        }
    }
}
