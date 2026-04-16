using Microsoft.EntityFrameworkCore;
using VesselCalc.Domain.Entities;

namespace VesselCalc.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Estas propriedades representam as tabelas no banco de dados
        public DbSet<Material> Materials { get; set; }
        public DbSet<MaterialAllowableStress> AllowableStresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Aqui configuramos restrições do banco (Fluent API)
            modelBuilder.Entity<Material>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Specification).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Grade).HasMaxLength(50);
                entity.Property(e => e.MinTensileStrength).HasColumnType("decimal(18,2)");
                entity.Property(e => e.MinYieldStrength).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<MaterialAllowableStress>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Temperature).HasColumnType("decimal(18,2)");
                entity.Property(e => e.StressValue).HasColumnType("decimal(18,2)");

                // Configura a Foreign Key explicitamente
                entity.HasOne(d => d.Material)
                      .WithMany(p => p.AllowableStresses)
                      .HasForeignKey(d => d.MaterialId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}