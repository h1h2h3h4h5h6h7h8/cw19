using App.Domain.Core.Memory.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Data.Db.SqlServer.Ef.Memory
{
    public class AppDbContaxt : DbContext
    {
        public DbSet<Member> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Member>()
                .HasKey(m => m.Id);

            modelBuilder.Entity<Member>()
                .Property(m => m.NationalCode)
                .HasMaxLength(10)
                .IsRequired(); 

            modelBuilder.Entity<Member>()
                .Property(m => m.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Member>()
                .Property(m => m.LastName)
                .IsRequired()
                .HasMaxLength(100); 

            modelBuilder.Entity<Member>()
                .Property(m => m.Score)
                .IsRequired();

            modelBuilder.Entity<Member>()
                .Property(m => m.BirthDate)
                .IsRequired(); 

            modelBuilder.Entity<Member>()
                .Property(m => m.RegisterDate)
                .IsRequired();

            modelBuilder.Entity<Member>()
                .Property(m => m.PhoneNumber)
                .HasMaxLength(15); 

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-61NFUIU\\SQLEXPRESS;Initial Catalog=CW19;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            }
        }

    }
}
