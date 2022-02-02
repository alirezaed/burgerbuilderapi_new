using Microsoft.EntityFrameworkCore;

namespace BurgerBuilderApi.Database
{
    public class BBContext: DbContext
    {
        public BBContext(DbContextOptions<BBContext> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<OrderModel> Orders { get; set; }
    }
}
