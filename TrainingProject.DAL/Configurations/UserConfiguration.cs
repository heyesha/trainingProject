﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainingProject.Domain.Entity;

namespace TrainingProject.DAL.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Login).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Password).IsRequired();

            builder.HasMany<Report>(x => x.Reports)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.User.Id)
                .HasPrincipalKey(x => x.Id);
        }
    }
}
