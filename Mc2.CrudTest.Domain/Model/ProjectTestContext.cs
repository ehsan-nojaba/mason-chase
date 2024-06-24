using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.DomainModel.Model
{
    public class ProjectTestContext:DbContext
    {
        public ProjectTestContext(DbContextOptions<ProjectTestContext> options) : base(options)
        {
            
        }

        public DbSet<Customer> Customers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Configuration.CustomerConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
