using Microsoft.EntityFrameworkCore.Storage;
using TrainingProject.Domain.Entity;
using TrainingProject.Domain.Interfaces.Databases;
using TrainingProject.Domain.Interfaces.Repositories;

namespace TrainingProject.DAL.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context, IBaseRepository<User> users, IBaseRepository<Role> roles, 
        IBaseRepository<UserRole> userRoles)
    {
        _context = context;
        Users = users;
        Roles = roles;
        UserRoles = userRoles;
    }

    public IBaseRepository<User> Users { get; set; }
    public IBaseRepository<Role> Roles { get; set; }
    public IBaseRepository<UserRole> UserRoles { get; set; }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _context.Database.BeginTransactionAsync();
    }
    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
