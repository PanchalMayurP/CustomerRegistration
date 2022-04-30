
namespace CustomerRegistration.Infrastructure;
public class CustomerDbContext : DbContext
{
    public CustomerDbContext()
    {

    }

    public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
    {
    }

    public DbSet<Customer> Customer { get; set; }
    public DbSet<FileDetails> FileDetails { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=AKM\\MAYURPANCHAL;Initial Catalog=SMS;Integrated Security=True");
    }
}
