namespace EventNet.Infrastructure
{
    public sealed class Context(DbContextOptions options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder builder) => builder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly).Seed();
    }
}
