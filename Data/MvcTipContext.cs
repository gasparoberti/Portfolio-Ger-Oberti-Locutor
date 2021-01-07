using Microsoft.EntityFrameworkCore;
using PortfolioCore.Models;

namespace PortfolioCore.Data
{
    public class MvcTipContext : DbContext
    {
        public MvcTipContext(DbContextOptions<MvcTipContext> options)
            : base(options)
        {
        }

        public DbSet<Tip> Tip { get; set; }
    }
}