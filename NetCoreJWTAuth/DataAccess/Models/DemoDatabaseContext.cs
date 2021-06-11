using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DataAccess.Models
{
    public partial class DemoDatabaseContext : DbContext
    {
        public DemoDatabaseContext()
        {
        }

        public DemoDatabaseContext(DbContextOptions<DemoDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<RefreshTokenId> RefreshTokenIds { get; set; }
        public virtual DbSet<UserInfo> UserInfos { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=DESKTOP-3DF0FM6\\SQLEXPRESS;Database=DemoDatabase;Trusted_Connection=True;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmpId)
                    .HasName("PK__Employee__AF2DBB99EEEB5EC1");

                entity.ToTable("Employee");

                entity.Property(e => e.EmpEmailId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmpName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Empaddress)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EMPAddress");
            });

            modelBuilder.Entity<RefreshTokenId>(entity =>
            {
                entity.HasKey(e => e.RefreshTokenId1)
                    .HasName("PK__RefreshT__F5845E397406A758");

                entity.ToTable("RefreshTokenId");

                entity.Property(e => e.RefreshTokenId1).HasColumnName("RefreshTokenId");

                entity.Property(e => e.ExpiryTime).HasColumnType("date");

                entity.Property(e => e.RefreshTokenValue)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RefreshTokenIds)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__RefreshTo__UserI__5EBF139D");
            });

            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__UserInfo__1788CC4CF7E64CF6");

                entity.ToTable("UserInfo");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Passw)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
