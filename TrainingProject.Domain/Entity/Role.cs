using System.Collections.Generic;
using TrainingProject.Domain.Interfaces;

namespace TrainingProject.Domain.Entity;

public class Role : IEntityId<long>
{
    public long Id { get; set; }

    public string Name { get; set; }

    public List<User> Users { get; set; }
}
