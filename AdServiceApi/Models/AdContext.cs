
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace AdServiceApi.Models
{
    public class AdContext : DbContext
    {
        public DbSet<AdModel> Ads { get; set; }
    }
}
