using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace UniverseSso.Entities
{
    public partial class LoginDbContext : DbContext
    {
        public LoginDbContext()
        {
        }

        public LoginDbContext(DbContextOptions<LoginDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AuthenticationStrategy> AuthenticationStrategy { get; set; }
        public virtual DbSet<Field> Field { get; set; }
        public virtual DbSet<Provider> Provider { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=LoginDb;Trusted_Connection=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthenticationStrategy>(entity =>
            {
                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasDefaultValueSql("(suser_name())");

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.StrategyName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UpdatedBy)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.UpdatedDatetime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Field>(entity =>
            {
                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasDefaultValueSql("(suser_name())");

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FieldName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.FieldType)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.PageType)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UpdatedBy)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.UpdatedDatetime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Provider)
                    .WithMany(p => p.Field)
                    .HasForeignKey(d => d.ProviderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Field__ProviderI__34C8D9D1");
            });

            modelBuilder.Entity<Provider>(entity =>
            {
                entity.HasIndex(e => e.ProviderName)
                    .HasName("UQ_ProviderName")
                    .IsUnique();

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasDefaultValueSql("(suser_name())");

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ProviderName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UpdatedBy)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.UpdatedDatetime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
