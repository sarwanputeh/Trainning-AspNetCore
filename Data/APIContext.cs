using Microsoft.EntityFrameworkCore;
using OrixNetCoreApp.Models;

namespace OrixNetCoreApp.Data
{
    public class APIContext: DbContext
    {
        public APIContext(DbContextOptions<APIContext> options)
            : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Product { get; set; }

    }
}
