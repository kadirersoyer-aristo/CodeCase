using BeymenCodeCase.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeymenCodeCase.Context
{
    public class StorageContext : DbContext
    {
        public DbSet<ApplicationConfiguration> Configuration { get; set; }
        public DbSet<ScheduleTask> ScheduleTask { get; set; }
        public DbSet<Logs> Logs { get; set; }
        public StorageContext(DbContextOptions<StorageContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationConfiguration>(entity =>
            {
                entity.Property(m => m.ApplicationName).HasMaxLength(200).IsRequired(true);
                entity.Property(m => m.IsActive).IsRequired(true);
                entity.Property(m => m.Name).HasMaxLength(200).IsRequired(true);
                entity.Property(m => m.Value).HasMaxLength(200).IsRequired(true);
            });

            modelBuilder.Entity<ScheduleTask>(entity =>
            {
                entity.Property(m => m.Enabled).IsRequired(true);
                entity.Property(m => m.LastErrorMessage).HasMaxLength(500).IsRequired(false);
                entity.Property(m => m.LastStartUtc).IsRequired(false);
                entity.Property(m => m.LastSuccessUtc).IsRequired(false);
                entity.Property(m => m.Name).HasMaxLength(100).IsRequired(true);
                entity.Property(m => m.Seconds).IsRequired(true);
                entity.Property(m => m.Type).HasMaxLength(100).IsRequired(false);
            });


            modelBuilder.Entity<Logs>(entity =>
            {
                //entity.Property(m => m.LogType).IsRequired(false);
                entity.Property(m => m.Message).HasMaxLength(500).IsRequired(false);
                entity.Property(m => m.ShortMessage).HasMaxLength(300).IsRequired(false);
                entity.Property(m => m.UpdateDate).IsRequired(false);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
