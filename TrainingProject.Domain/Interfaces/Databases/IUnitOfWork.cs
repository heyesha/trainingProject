using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;
using TrainingProject.Domain.Entity;
using TrainingProject.Domain.Interfaces.Repositories;

namespace TrainingProject.Domain.Interfaces.Databases;

public interface IUnitOfWork : IStateSaveChanges
{
    Task<IDbContextTransaction> BeginTransactionAsync();

    IBaseRepository<User> Users { get; set; }

    IBaseRepository<Role> Roles { get; set; }

    IBaseRepository<UserRole> UserRoles { get; set; }
}
