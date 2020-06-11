using Microsoft.EntityFrameworkCore;
using PortfolioCore.Models;

namespace PortfolioCore.Data
{
    public class MvcSobreMiContext : DbContext
    {
        public MvcSobreMiContext(DbContextOptions<MvcSobreMiContext> options)
            : base(options)
        {
        }

        public DbSet<SobreMi> SobreMi { get; set; }
    }
}
