using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;

using SimpleCarRepair.Models.SimpleCarRepairDb;

namespace SimpleCarRepair.Data
{
  public partial class SimpleCarRepairDbContext : Microsoft.EntityFrameworkCore.DbContext
  {
    public SimpleCarRepairDbContext(DbContextOptions<SimpleCarRepairDbContext> options):base(options)
    {
    }

    public SimpleCarRepairDbContext()
    {
    }

    partial void OnModelBuilding(ModelBuilder builder);

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<SimpleCarRepair.Models.SimpleCarRepairDb.PartReapir>()
              .HasOne(i => i.Part)
              .WithMany(i => i.PartReapirs)
              .HasForeignKey(i => i.PartId)
              .HasPrincipalKey(i => i.Id);
        builder.Entity<SimpleCarRepair.Models.SimpleCarRepairDb.PartReapir>()
              .HasOne(i => i.Repair)
              .WithMany(i => i.PartReapirs)
              .HasForeignKey(i => i.RepairId)
              .HasPrincipalKey(i => i.Id);
        builder.Entity<SimpleCarRepair.Models.SimpleCarRepairDb.Repair>()
              .HasOne(i => i.Client)
              .WithMany(i => i.Repairs)
              .HasForeignKey(i => i.ClientId)
              .HasPrincipalKey(i => i.Id);

        builder.Entity<SimpleCarRepair.Models.SimpleCarRepairDb.PartReapir>()
              .Property(p => p.Quantity)
              .HasDefaultValueSql("((0))");

        builder.Entity<SimpleCarRepair.Models.SimpleCarRepairDb.Repair>()
              .Property(p => p.Finished)
              .HasDefaultValueSql("(CONVERT([bit],(0)))");

        builder.Entity<SimpleCarRepair.Models.SimpleCarRepairDb.Repair>()
              .Property(p => p.Deactivated)
              .HasDefaultValueSql("(CONVERT([bit],(0)))");


        builder.Entity<SimpleCarRepair.Models.SimpleCarRepairDb.Repair>()
              .Property(p => p.Date)
              .HasColumnType("datetime2");

        builder.Entity<SimpleCarRepair.Models.SimpleCarRepairDb.Client>()
              .Property(p => p.Id)
              .HasPrecision(10, 0);

        builder.Entity<SimpleCarRepair.Models.SimpleCarRepairDb.Part>()
              .Property(p => p.Id)
              .HasPrecision(10, 0);

        builder.Entity<SimpleCarRepair.Models.SimpleCarRepairDb.Part>()
              .Property(p => p.Cost)
              .HasPrecision(18, 2);

        builder.Entity<SimpleCarRepair.Models.SimpleCarRepairDb.PartReapir>()
              .Property(p => p.Id)
              .HasPrecision(10, 0);

        builder.Entity<SimpleCarRepair.Models.SimpleCarRepairDb.PartReapir>()
              .Property(p => p.PartId)
              .HasPrecision(10, 0);

        builder.Entity<SimpleCarRepair.Models.SimpleCarRepairDb.PartReapir>()
              .Property(p => p.RepairId)
              .HasPrecision(10, 0);

        builder.Entity<SimpleCarRepair.Models.SimpleCarRepairDb.PartReapir>()
              .Property(p => p.Quantity)
              .HasPrecision(10, 0);

        builder.Entity<SimpleCarRepair.Models.SimpleCarRepairDb.Repair>()
              .Property(p => p.Id)
              .HasPrecision(10, 0);

        builder.Entity<SimpleCarRepair.Models.SimpleCarRepairDb.Repair>()
              .Property(p => p.ClientId)
              .HasPrecision(10, 0);
        this.OnModelBuilding(builder);
    }


    public DbSet<SimpleCarRepair.Models.SimpleCarRepairDb.Client> Clients
    {
      get;
      set;
    }

    public DbSet<SimpleCarRepair.Models.SimpleCarRepairDb.Part> Parts
    {
      get;
      set;
    }

    public DbSet<SimpleCarRepair.Models.SimpleCarRepairDb.PartReapir> PartReapirs
    {
      get;
      set;
    }

    public DbSet<SimpleCarRepair.Models.SimpleCarRepairDb.Repair> Repairs
    {
      get;
      set;
    }
  }
}
