using Microsoft.EntityFrameworkCore;
using Modelos;

namespace ConexusHeinerUrennaZunniga.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        public DbSet<Personas> personas { get; set; }
        public DbSet<Productos> productos { get; set; }
    }
}
