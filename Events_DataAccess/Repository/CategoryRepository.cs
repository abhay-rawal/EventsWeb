using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Events_Data;
using Events_Repository;
using Events_Repository.Data;
using Events_Repository.Repository.IRepository;
using EventsWeb.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace Events_Repository.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public CategoryRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<EventsCategory> Create(EventsCategory objDTO)
        {
            var obj = _mapper.Map<EventsCategory, Category>(objDTO);
            var addedObj = _db.Categories.Add(obj);
            await _db.SaveChangesAsync();
            return _mapper.Map<Category, EventsCategory>(addedObj.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var obj = await _db.Categories.FirstOrDefaultAsync(u => u.Id == id);
            if (obj != null)
            {
                _db.Categories.Remove(obj);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<EventsCategory> Get(int id)
        {
            var obj = await _db.Categories.FirstOrDefaultAsync(u => u.Id == id);
            if (obj != null)
            {
                return _mapper.Map<Category, EventsCategory>(obj);
            }
            return new EventsCategory();
        }

        public async Task<IEnumerable<EventsCategory>> GetAll()
        {
            return _mapper.Map<IEnumerable<Category>, IEnumerable<EventsCategory>>(_db.Categories);
        }
       
        public async Task<EventsCategory> Update(EventsCategory objDTO)
        {
            var objFromDb = await _db.Categories.FirstOrDefaultAsync(u => u.Id == objDTO.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = objDTO.Name;
                objFromDb.Size = objDTO.Size;
                objFromDb.Description = objDTO.Description;
                objFromDb.ImageUrl = objDTO.ImageUrl;

                _db.Categories.Update(objFromDb);
                await _db.SaveChangesAsync();
                return _mapper.Map<Category, EventsCategory>(objFromDb);
            }
            return objDTO;
        }
    }
}
