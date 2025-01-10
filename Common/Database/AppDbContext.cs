using System;
using DigilizeTest.Companies.Models;
using DigilizeTest.Users.Models;
using Microsoft.EntityFrameworkCore;

namespace DigilizeTest.Common.Database;

public class AppDbContext :DbContext
{
    public AppDbContext()
    {
        
    }

    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {
        
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Company> Companies { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().HasKey(m=>m.Id);
        modelBuilder.Entity<User>().Property(m=>m.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        modelBuilder.Entity<Company>().HasKey(m=>m.Id);
        modelBuilder.Entity<Company>().Property(m=>m.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

    }
}
