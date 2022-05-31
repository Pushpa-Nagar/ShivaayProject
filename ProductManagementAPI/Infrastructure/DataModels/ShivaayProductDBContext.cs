using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ProductManagementAPI.Infrastructure.DataModels
{
    public partial class ShivaayProductDBContext : DbContext
    {
        public ShivaayProductDBContext(DbContextOptions<ShivaayProductDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Agreements> Agreements { get; set; }
        public virtual DbSet<ProductGroup> ProductGroup { get; set; }
        public virtual DbSet<Products> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=172.29.19.137;Database=ShivaayProductDB;TrustServerCertificate=True; user id = Sofyx; password = 4zMD78xl;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agreements>(entity =>
            {
                entity.HasKey(e => e.AgreementId);

                entity.Property(e => e.AgreementId).HasColumnName("AgreementID");

                entity.Property(e => e.EffectiveDate).HasColumnType("datetime");

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.NewPrice).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ProductGroupId).HasColumnName("ProductGroupID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.ProductPrice).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("UserID")
                    .HasMaxLength(450);

                entity.HasOne(d => d.ProductGroup)
                    .WithMany(p => p.Agreements)
                    .HasForeignKey(d => d.ProductGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AgreementProductGroup");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Agreements)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AgreementProducts");
            });

            modelBuilder.Entity<ProductGroup>(entity =>
            {
                entity.HasIndex(e => e.GroupCode)
                    .HasName("UQ__ProductG__3B97438094747C0A")
                    .IsUnique();

                entity.Property(e => e.ProductGroupId).HasColumnName("ProductGroupID");

                entity.Property(e => e.GroupCode)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.GroupDescription)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.HasIndex(e => e.ProductNumber)
                    .HasName("UQ__Products__49A3C8397931C7DC")
                    .IsUnique();

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ProductDescription)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ProductGroupId).HasColumnName("ProductGroupID");

                entity.Property(e => e.ProductNumber)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.ProductGroup)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductGroup");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
