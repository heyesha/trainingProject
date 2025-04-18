﻿using TrainingProject.Domain.Interfaces.Repositories;

namespace TrainingProject.DAL.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _dbContext;

        public BaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity is NULL");
            }
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        // IQueryable, чтобы операции фильтровки, сортировки можно было производить на уровне БД, а не на уровне приложения
        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>();
        }

        public void Remove(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity is NULL");
            }
            _dbContext.Remove(entity);
        }


        public TEntity Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity is NULL");
            }
            _dbContext.Update(entity);

            return entity;
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
