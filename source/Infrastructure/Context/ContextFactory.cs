namespace EventNet.Infrastructure
{
    public sealed class ContextFactory : IDesignTimeDbContextFactory<Context>
    {
        public Context CreateDbContext(string[] args)
        {
            var connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=masterpassword";

            return new Context(new DbContextOptionsBuilder<Context>().UseNpgsql(connectionString).Options);
        }
    }
}
