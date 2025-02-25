using Microsoft.EntityFrameworkCore;
using stock_market.Models;

namespace stock_market.Data;

public class AppDbContext : DbContext
{
        public AppDbContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {

        }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }
}

