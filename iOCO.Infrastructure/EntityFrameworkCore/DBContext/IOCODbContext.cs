using iOCO.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.IO;

namespace iOCO.Infrastructure.EntityFrameworkCore.DBContext
{
    public class IOCODbContext : DbContext
    {
        public IOCODbContext()
        {
                
        }

        public IOCODbContext(DbContextOptions<IOCODbContext> options)
        : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.ToTable("Inventory");

                entity.Property(e => e.InventoryName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.Survivor)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.SurvivorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Inventory_Survivor");
            });

            modelBuilder.Entity<Survivor>(entity =>
            {
                entity.ToTable("Survivor");

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Latitude).HasMaxLength(50);

                entity.Property(e => e.Longitude)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<ContaminatedSurvivor>(entity =>
            {
                entity.ToTable("ContaminatedSurvivor");

                entity.Property(e => e.ReporterSurvivorId)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }

        public DbSet<ContaminatedSurvivor> ContaminatedSurvivor { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<Survivor> Survivor { get; set; }
    }
}
