using Microsoft.EntityFrameworkCore;
using MySqlContextFactory.Data.MyDatabase;

namespace MySqlContextFactory.Services
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAll2Async(Func<T, bool> filter);
        Task<T?> GetByIdAsync(object id);
        Task<T> InsertAsync(T obj);
        Task UpdateAsync(T obj);
        Task DeleteAsync(object id);
    }


    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        readonly IDbContextFactory<MyDatabaseDbContext> dbContextFactory;

        public GenericRepository(IDbContextFactory<MyDatabaseDbContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        public async Task<List<T>> GetAllAsync()
        {
            using var db = dbContextFactory.CreateDbContext();
            var m = db.Set<T>();
            var x = m.ToList();
            return await db.Set<T>()
                           .AsNoTracking()
                           .ToListAsync();
        }

        public async Task<List<T>> GetAll2Async(Func<T, bool> filter)
        {
            using var db = dbContextFactory.CreateDbContext();
            var m = db.Set<T>();
            var x = m.Where(filter).ToList();
            return await db.Set<T>()
                           .AsNoTracking()
                           .ToListAsync();
        }

        public async Task<T?> GetByIdAsync(object id)
        {
            using var db = dbContextFactory.CreateDbContext();
            return await db.Set<T>().FindAsync(id);
        }

        public async Task<T> InsertAsync(T obj)
        {
            using var db = dbContextFactory.CreateDbContext();
            db.Set<T>().Add(obj);
            await db.SaveChangesAsync();
            return obj;
        }

        public async Task UpdateAsync(T obj)
        {
            using var db = dbContextFactory.CreateDbContext();
            db.Set<T>().Update(obj);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(object id)
        {
            using var db = dbContextFactory.CreateDbContext();
            DbSet<T> table = db.Set<T>();
            T? existingRecord = await table.FindAsync(id);
            if (existingRecord != null)
            {
                table.Remove(existingRecord);
                await db.SaveChangesAsync();
            }
        }
    }
}
