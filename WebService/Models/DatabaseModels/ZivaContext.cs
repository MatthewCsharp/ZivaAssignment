using Microsoft.EntityFrameworkCore;

namespace WebService.Models.DatabaseModels {
    public class ZivaContext : DbContext {
        public ZivaContext(DbContextOptions<ZivaContext> options) : base(options) {
        }

        public virtual DbSet<UserStats> UserStats { get; set; }
    }
}