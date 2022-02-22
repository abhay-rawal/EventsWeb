using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsWeb.Shared.Model;

namespace Events_Repository.Repository.IRepository
{
    public interface ICategoryRepository
    {
   
        public Task<EventsCategory> Create(EventsCategory objDTO);
        public Task<EventsCategory> Update(EventsCategory objDTO);
        public Task<int> Delete(int id);
        public Task<EventsCategory> Get(int id);
        public Task<IEnumerable<EventsCategory>> GetAll();

    }
}
