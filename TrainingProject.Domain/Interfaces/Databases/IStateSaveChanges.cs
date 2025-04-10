using System.Threading.Tasks;

namespace TrainingProject.Domain.Interfaces.Databases;

public interface IStateSaveChanges
{
    Task<int> SaveChangesAsync();
}
