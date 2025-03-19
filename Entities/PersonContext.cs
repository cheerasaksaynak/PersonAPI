using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PersonAPI.Entities;

public partial class PersonContext : DbContext
{
    public PersonContext(DbContextOptions<PersonContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Person> Person { get; set; }

    public virtual DbSet<PersonBasicInfo> PersonBasicInfo { get; set; }

    public virtual DbSet<PersonProfileInfo> PersonProfileInfo { get; set; }

    public virtual DbSet<PersonSalaryInfo> PersonSalaryInfo { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("person_pkey");
        });

        modelBuilder.Entity<PersonBasicInfo>(entity =>
        {
            entity.ToView("person_basic_info");
        });

        modelBuilder.Entity<PersonProfileInfo>(entity =>
        {
            entity.ToView("person_profile_info");
        });

        modelBuilder.Entity<PersonSalaryInfo>(entity =>
        {
            entity.ToView("person_salary_info");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
