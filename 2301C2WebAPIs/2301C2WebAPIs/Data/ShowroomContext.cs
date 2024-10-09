using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using _2301C2WebAPIs.Models;

namespace _2301C2WebAPIs.Data;

public partial class ShowroomContext : DbContext
{
    public ShowroomContext()
    {
    }

    public ShowroomContext(DbContextOptions<ShowroomContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=showroom;User ID=sa;Password=aptech;Encrypt=False;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cars__3214EC07C2713286");

            entity.Property(e => e.Color).HasMaxLength(60);
            entity.Property(e => e.Model).HasMaxLength(60);
            entity.Property(e => e.Name).HasMaxLength(60);
            entity.Property(e => e.Power).HasMaxLength(60);

            entity.HasOne(d => d.Manufacturer).WithMany(p => p.Cars)
                .HasForeignKey(d => d.ManufacturerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cars_ToManufacturer");
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC07ABE33999");

            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(60);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
