using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DemoSignalR.Models
{
    public partial class ThongKeCovidContext : DbContext
    {
        public ThongKeCovidContext()
        {
        }

        public ThongKeCovidContext(DbContextOptions<ThongKeCovidContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CaBenh> CaBenhs { get; set; }
        public virtual DbSet<TinhThanh> TinhThanhs { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-LS5JMBQ\\SQLEXPRESS; Database=ThongKeCovid;Trusted_Connection=False;User ID=sa;Password=123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<CaBenh>(entity =>
            {
                entity.HasKey(e => new { e.MaTinh, e.Id });

                entity.ToTable("CaBenh");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.NgayCapNhat).HasColumnType("date");

                entity.HasOne(d => d.IdNavigation)
                    .WithMany(p => p.CaBenhs)
                    .HasForeignKey(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CaBenh_User");

                entity.HasOne(d => d.MaTinhNavigation)
                    .WithMany(p => p.CaBenhs)
                    .HasForeignKey(d => d.MaTinh)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CaBenh_TinhThanh");
            });

            modelBuilder.Entity<TinhThanh>(entity =>
            {
                entity.HasKey(e => e.MaTinh);

                entity.ToTable("TinhThanh");

                entity.Property(e => e.MaTinh).ValueGeneratedNever();

                entity.Property(e => e.TenTinh)
                    .HasMaxLength(100)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsFixedLength(true);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsFixedLength(true);

                entity.Property(e => e.Password)
                    .HasMaxLength(20)
                    .IsFixedLength(true);

                entity.Property(e => e.UserId)
                    .HasMaxLength(20)
                    .HasColumnName("UserID")
                    .IsFixedLength(true);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
