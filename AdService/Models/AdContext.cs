using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdService.Models
{
    public class AdContext : DbContext
    {
        public DbSet<Ad> Ads { get; set; }
        public AdContext(DbContextOptions<AdContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
