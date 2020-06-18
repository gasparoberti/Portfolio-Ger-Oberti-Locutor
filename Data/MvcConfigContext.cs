using Microsoft.EntityFrameworkCore;
using PortfolioCore.Models;

namespace PortfolioCore.Data
{
    public class MvcConfigContext : DbContext
    {
        public MvcConfigContext(DbContextOptions<MvcConfigContext> options)
            : base(options)
        {
        }

        public DbSet<Config> Config { get; set; }
    }
}
