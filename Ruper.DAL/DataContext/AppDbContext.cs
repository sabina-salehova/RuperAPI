using Microsoft.EntityFrameworkCore;
using Ruper.DAL.Entities;

namespace Ruper.DAL.DataContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Slider> Sliders { get; set; }
    }
}
