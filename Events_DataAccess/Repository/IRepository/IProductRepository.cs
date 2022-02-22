using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsWeb.Shared.Model;

namespace Events_Repository.Repository.IRepository
{
    public interface IProductRepository
    {
   
        public Task<EventsProduct> Create(EventsProduct objDTO);
        public Task<EventsProduct> Update(EventsProduct objDTO);
        public Task<int> Delete(int id);
        public Task<EventsProduct> Get(int id);
        public Task<IEnumerable<EventsProduct>> GetAll();

    }
}
