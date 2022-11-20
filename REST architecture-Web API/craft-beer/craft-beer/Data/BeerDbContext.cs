using craft_beer.Models;
using Microsoft.EntityFrameworkCore;

namespace craft_beer.Data
{
    public class BeerDbContext: DbContext
    {
        public BeerDbContext(DbContextOptions<BeerDbContext> options): base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<beer>()
                .HasOne<BeerStyle>(b => b.Style)
                .WithMany(be => be.Beers)
                .HasForeignKey(b => b.BeerStyleId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<beer> Beers { get; set; }
        public DbSet<BeerStyle> BeerStyles { get; set; }

        
    }
}
