using Microsoft.EntityFrameworkCore;
using PortfolioCore.Models;

namespace PortfolioCore.Data
{
    public class MvcRelatoContext : DbContext
    {
        public MvcRelatoContext(DbContextOptions<MvcRelatoContext> options)
            : base(options)
        {
        }

        public DbSet<Relato> Relato { get; set; }

        public DbSet<PortfolioCore.Models.Tip> Tip { get; set; }
    }
}
