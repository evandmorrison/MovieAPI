using Microsoft.EntityFrameworkCore;
using MovieAPI.Database.Models;

public interface IDBContext
{
    DbSet<Movie> Movie { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
}

public class Context : DbContext, IDBContext
{
    public Context(DbContextOptions<Context> options) : base(options)
    {
        this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }

    public DbSet<Movie> Movie { get; set; }
}