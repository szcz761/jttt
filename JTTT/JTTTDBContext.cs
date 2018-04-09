using System.Data.Entity;

namespace JTTT
{
    public class JTTTDBContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }

    }
}

