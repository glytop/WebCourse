using Microsoft.EntityFrameworkCore;

namespace Itransition.Trainee.Web.Data
{
    public class WebDbContext : DbContext
    {
        
        
        
        public WebDbContext(DbContextOptions<WebDbContext> contextOptions)
            : base(contextOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
