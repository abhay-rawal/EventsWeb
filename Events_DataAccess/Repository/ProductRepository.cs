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
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public ProductRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<EventsProduct> Create(EventsProduct objDTO)
        {
            var obj = _mapper.Map<EventsProduct, Product>(objDTO);
            var addedObj = _db.Products.Add(obj);
            await _db.SaveChangesAsync();
            return _mapper.Map<Product, EventsProduct>(addedObj.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var obj = await _db.Products.FirstOrDefaultAsync(u => u.Id == id);
            if (obj != null)
            {
                _db.Products.Remove(obj);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<EventsProduct> Get(int id)
        {
            var obj = await _db.Products.FirstOrDefaultAsync(u => u.Id == id);
            if (obj != null)
            {
                return _mapper.Map<Product, EventsProduct>(obj);
            }
            return new EventsProduct();
        }

        public async Task<IEnumerable<EventsProduct>> GetAll()
        {
            return _mapper.Map<IEnumerable<Product>, IEnumerable<EventsProduct>>(_db.Products.Include(u=>u.Category));
        }
       
        public async Task<EventsProduct> Update(EventsProduct objDTO)
        {
            var objFromDb = await _db.Products.FirstOrDefaultAsync(u => u.Id == objDTO.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = objDTO.Name;
                objFromDb.Organisedby = objDTO.Organisedby;
                objFromDb.StartsAt = objDTO.StartsAt;
                objFromDb.EndsAt = objDTO.EndsAt;
                objFromDb.Name = objDTO.Name;
                objFromDb.Location = objDTO.Location;
                objFromDb.Description = objDTO.Description;
                objFromDb.Price = objDTO.Price;
                objFromDb.CategoryId = objDTO.CategoryId;

                _db.Products.Update(objFromDb);
                await _db.SaveChangesAsync();
                return _mapper.Map<Product, EventsProduct>(objFromDb);
            }
            return objDTO;
        }
    }
}
