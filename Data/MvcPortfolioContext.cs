using Microsoft.EntityFrameworkCore;
using PortfolioCore.Models;

namespace PortfolioCore.Data
{
    public class MvcPortfolioContext : DbContext
    {
        public MvcPortfolioContext(DbContextOptions<MvcPortfolioContext> options)
            : base(options)
        {
        }

        public DbSet<Portfolio> Portfolio { get; set; }
    }
}
