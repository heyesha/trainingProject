using System.Linq;
using System.Threading.Tasks;
using TrainingProject.Domain.Interfaces.Databases;

namespace TrainingProject.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity> : IStateSaveChanges
    {
        IQueryable<TEntity> GetAll();

        Task<TEntity> CreateAsync(TEntity entity);

        TEntity Update(TEntity entity);

        void Remove(TEntity entity);
    }
}
