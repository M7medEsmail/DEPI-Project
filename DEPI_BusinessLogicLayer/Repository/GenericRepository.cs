using DEPI_BusinessLogicLayer.IRepository;
using DEPI_DomainLayer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_BusinessLogicLayer.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly StoreDbContext _context; // Protected because I Used it in ProductRepository
        private readonly DbSet<T> _dbSet;

        public GenericRepository(StoreDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);

        }


        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            _context.SaveChanges();
        }

        public async Task<bool> UpdateAsync(int id, T entity)
        {
            // Await the result of GetByIdAsync
            var existingEntity = await GetByIdAsync(id);

            // Check if the entity exists
            if (existingEntity != null)
            {
                // Update the entity
                _context.Set<T>().Update(entity); // Only update the modified properties
                                                  // Alternatively: _context.Entry(entity).State = EntityState.Modified;

                // Save changes asynchronously
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
                return true; // Indicate successful deletion
            }
            return false; // Indicate entity was not found


        }
    }
}
