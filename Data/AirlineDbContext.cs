using AirlineApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AirlineApi.Data
{
    public class AirlineDbContext : DbContext
    {
        public AirlineDbContext(DbContextOptions<AirlineDbContext> options) : base(options)
        {

        }

        public DbSet<WebhookSubscription> WebhoookSubsriptions { get; set; }
    }
}