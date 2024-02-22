using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TestWebApi.Models
{
    public partial class TestWebApiContext : DbContext
    {
        public TestWebApiContext()
        {
        }

        public TestWebApiContext(DbContextOptions<TestWebApiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ReserveInfo> ReserveInfo { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReserveInfo>(entity =>
            {
                entity.Property(e => e.ID).HasComment("預約號碼");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasComment("建立時間");

                entity.Property(e => e.NumberOfPeople).HasComment("預約人數");

                entity.Property(e => e.ReserveDate)
                    .HasColumnType("date")
                    .HasComment("預約日期");

                entity.Property(e => e.ReserveUserName)
                    .HasMaxLength(100)
                    .HasComment("預約人員姓名");

                entity.Property(e => e.ReserveUserPhone)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("預約人員電話");

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasComment("更新時間");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
