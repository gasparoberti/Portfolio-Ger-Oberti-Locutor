using Microsoft.EntityFrameworkCore;
using PortfolioCore.Models;

namespace PortfolioCore.Data
{
    public class MvcPodcastContext : DbContext
    {
        public MvcPodcastContext(DbContextOptions<MvcPodcastContext> options)
            : base(options)
        {
        }

        public DbSet<Podcast> Podcast { get; set; }
    }
}
