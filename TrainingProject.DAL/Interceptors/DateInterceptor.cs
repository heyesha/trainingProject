using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TrainingProject.Domain.Interfaces;

namespace TrainingProject.DAL.Interceptors
{
    public class DateInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            var dbContext = eventData.Context;
            if (dbContext == null)
            {
                return base.SavingChanges(eventData, result);
            }

            var entries = dbContext.ChangeTracker.Entries<IAuditable>();
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property(x => x.CreatedAt).CurrentValue = DateTime.UtcNow;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property(x => x.UpdatedAt).CurrentValue = DateTime.UtcNow;
                }
            }

            return base.SavingChanges(eventData, result);
        }
    }
}
