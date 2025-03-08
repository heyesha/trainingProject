using TrainingProject.Domain.Interfaces.Repositories;

namespace TrainingProject.DAL.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _dbContext;

        public BaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<TEntity> CreateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity is NULL");
            }
            _dbContext.Add(entity);
            _dbContext.SaveChanges();

            return Task.FromResult(entity);
        }

        // IQueryable, чтобы операции фильтровки, сортировки можно было производить на уровне БД, а не на уровне приложения
        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>();
        }

        public Task<TEntity> RemoveAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity is NULL");
            }
            _dbContext.Remove(entity);
            _dbContext.SaveChanges();

            return Task.FromResult(entity);
        }

        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity is NULL");
            }
            _dbContext.Update(entity);
            _dbContext.SaveChanges();

            return Task.FromResult(entity);
        }
    }
}
