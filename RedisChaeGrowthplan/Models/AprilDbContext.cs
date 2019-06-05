using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisChaeGrowthplan.Models
{
    public class AprilDbContext : DbContext
    {

        public AprilDbContext(DbContextOptions<AprilDbContext> options) : base(options)
        {

        }

        public DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Users>(b =>
            {
                b.Property<bool>("IsDeleted").HasColumnType("bit");
                b.Property<bool>("Sex").HasColumnType("bit");
                //b.Property<bool>("EmailConfirmed").HasColumnType("bit");
                ////b.Property<bool>("LockoutEnabled").HasColumnType("bit");
                //b.Property<bool>("PhoneNumberConfirmed").HasColumnType("bit");
                //b.Property<bool>("TwoFactorEnabled").HasColumnType("bit");
                //b.Property<bool>("IsDisplay").HasColumnType("bit");
                b.ToTable("identityuser");
            });


        }
    }
}
