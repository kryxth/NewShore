using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace NewShoreData
{
    public partial class NewShoreBDContext : DbContext
    {
        public NewShoreBDContext()
        {
        }

        public NewShoreBDContext(DbContextOptions<NewShoreBDContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Flight> Flights { get; set; }
        public virtual DbSet<Journey> Journeys { get; set; }
        public virtual DbSet<JourneyFlight> JourneyFlights { get; set; }
        public virtual DbSet<Transport> Transports { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("data source=DESKTOP-7T3SDNA\\SQLEXPRESS;persist security info=False;initial catalog=NewShoreBD;integrated security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Flight>(entity =>
            {
                entity.HasKey(e => e.IdFlight);

                entity.ToTable("Flight");

                entity.Property(e => e.Destination)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Origin)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.HasOne(d => d.TransportNavigation)
                    .WithMany(p => p.Flights)
                    .HasForeignKey(d => d.Transport)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Flight_Transport");
            });

            modelBuilder.Entity<Journey>(entity =>
            {
                entity.HasKey(e => e.IdJourney);

                entity.ToTable("Journey");

                entity.Property(e => e.Destination)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Origin)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<JourneyFlight>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("JourneyFlight");

                entity.HasOne(d => d.FlightNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Flight)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JourneyFlight_Flight");

                entity.HasOne(d => d.JourneyNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Journey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JourneyFlight_Journey");
            });

            modelBuilder.Entity<Transport>(entity =>
            {
                entity.HasKey(e => e.IdTransport);

                entity.ToTable("Transport");

                entity.Property(e => e.FlightCarrier)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FlightNumber)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
