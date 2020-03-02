using System;
using BackgroundService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BackgroundService.Domain
{
    public class BackgroundServiceDbContext : DbContext
    {
        public BackgroundServiceDbContext(DbContextOptions<BackgroundServiceDbContext> options)
            : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Item>().HasKey(x => x.Id);
        }
    }
}
