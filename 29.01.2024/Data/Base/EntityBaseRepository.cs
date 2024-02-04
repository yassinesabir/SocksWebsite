using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;


namespace SocksWebsite.Data.Base
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        private readonly AppDbContext _context;
        public EntityBaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(n => n.Id == id);
#pragma warning disable CS8604 // Existence possible d'un argument de référence null.
            EntityEntry entityEntry = _context.Entry<T>(entity);
#pragma warning restore CS8604 // Existence possible d'un argument de référence null.
            entityEntry.State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = (IQueryable<T>)_context.Set<T>();
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return await query.ToListAsync();

        }

#pragma warning disable CS8603 // Existence possible d'un retour de référence null.
        public async Task<T> GetByIdAsync(int id) => await _context.Set<T>().FirstOrDefaultAsync(n => n.Id == id);
#pragma warning restore CS8603 // Existence possible d'un retour de référence null.

        public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = (IQueryable<T>)_context.Set<T>();
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
#pragma warning disable CS8603 // Existence possible d'un retour de référence null.
            return await query.FirstOrDefaultAsync(n => n.Id == id);
#pragma warning restore CS8603 // Existence possible d'un retour de référence null.
        }


        public async Task UpdateAsync(int id, T entity)
        {
            var existingEntity = await _context.Set<T>().FirstOrDefaultAsync(n => n.Id == id);

            if (existingEntity != null)
            {
                _context.Entry(existingEntity).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}