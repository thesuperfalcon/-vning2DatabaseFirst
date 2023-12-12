using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Övning2DatabaseFirst.Models;

public partial class Parking10Context : DbContext
{
    public Parking10Context()
    {
    }

    public Parking10Context(DbContextOptions<Parking10Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<ParkingHouse> ParkingHouses { get; set; }

    public virtual DbSet<ParkingSlot> ParkingSlots { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=Parking10;Trusted_Connection=True;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cars__3214EC071F58442A");

            entity.HasIndex(e => e.Plate, "UQ__Cars__830E47DCF2F138E6").IsUnique();

            entity.Property(e => e.Color)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Make)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Plate)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.ParkingSlots).WithMany(p => p.Cars)
                .HasForeignKey(d => d.ParkingSlotsId)
                .HasConstraintName("FK_ParkingSlotsCar");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cities__3214EC076B409569");

            entity.Property(e => e.CityName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ParkingHouse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ParkingH__3214EC07899465B2");

            entity.Property(e => e.HouseName)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.City).WithMany(p => p.ParkingHouses)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK_CityParkingHouse");
        });

        modelBuilder.Entity<ParkingSlot>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ParkingS__3214EC073BB15F90");

            entity.HasOne(d => d.ParkingHouse).WithMany(p => p.ParkingSlots)
                .HasForeignKey(d => d.ParkingHouseId)
                .HasConstraintName("FK_ParkingHouseParkingSlots");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
